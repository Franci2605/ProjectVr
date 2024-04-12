
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static PixelizeFeature;
using static UnityEngine.Experimental.Rendering.RayTracingAccelerationStructure;
using static UnityEngine.Experimental.Rendering.Universal.RenderObjects;

public class PixelizePass : ScriptableRenderPass
{
    private CustomPassSettings settings;

    private RenderTargetIdentifier colorBuffer, pixelBuffer;
    private int pixelBufferID = Shader.PropertyToID("_PixelBuffer");

    //private RenderTargetIdentifier pointBuffer;
    //private int pointBufferID = Shader.PropertyToID("_PointBuffer");
    private readonly ShaderTagId shaderTag = new ShaderTagId(nameof(PixelizePass));
    private Material material;
    private int pixelScreenHeight, pixelScreenWidth;
    private FilteringSettings _filteringSettings;

    private static readonly ShaderTagId _srpDefaultUnlit = new ShaderTagId("SRPDefaultUnlit");
    private static readonly ShaderTagId _universalForward = new ShaderTagId("UniversalForward");
    private static readonly ShaderTagId _lightweightForward = new ShaderTagId("LightweightForward");

    private static readonly List<ShaderTagId> _shaderTags = new List<ShaderTagId>
    {
        _srpDefaultUnlit, _universalForward, _lightweightForward,
    };
    private readonly Material _outlineMaterial;


    public PixelizePass(CustomPassSettings settings)
    {
        this.settings = settings;
        this.renderPassEvent = settings.renderPassEvent;
        if (material == null) material = CoreUtils.CreateEngineMaterial("Hidden/Pixelize");

        _filteringSettings = new FilteringSettings(RenderQueueRange.all, settings.LayerMask.value);

    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

        //cmd.GetTemporaryRT(pointBufferID, descriptor.width, descriptor.height, 0, FilterMode.Point);
        //pointBuffer = new RenderTargetIdentifier(pointBufferID);

        pixelScreenHeight = settings.screenHeight;
        pixelScreenWidth = (int)(pixelScreenHeight * renderingData.cameraData.camera.aspect + 0.5f);

        material.SetVector("_BlockCount", new Vector2(pixelScreenWidth, pixelScreenHeight));
        material.SetVector("_BlockSize", new Vector2(1.0f / pixelScreenWidth, 1.0f / pixelScreenHeight));
        material.SetVector("_HalfBlockSize", new Vector2(0.5f / pixelScreenWidth, 0.5f / pixelScreenHeight));

        descriptor.height = pixelScreenHeight;
        descriptor.width = pixelScreenWidth;

        cmd.GetTemporaryRT(pixelBufferID, descriptor, FilterMode.Point);
        pixelBuffer = new RenderTargetIdentifier(pixelBufferID);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        DrawingSettings drawingSettings = CreateDrawingSettings(
           _shaderTags,
           ref renderingData,
           settings.SortingCriteria
       );
        drawingSettings.overrideMaterial = _outlineMaterial;
        drawingSettings.overrideMaterialPassIndex = 1;


        // TODO: Switch to this once mismatched markers bug is fixed.
        // CommandBuffer cmd = CommandBufferPool.Get(ProfilerTag);
        CommandBuffer cmd = CommandBufferPool.Get();

        using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass")))
        {
            // No-shader variant
            //Blit(cmd, colorBuffer, pointBuffer);
            //Blit(cmd, pointBuffer, pixelBuffer);
            //Blit(cmd, pixelBuffer, colorBuffer);
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref _filteringSettings);

            Blit(cmd, colorBuffer, pixelBuffer, material);
            Blit(cmd, pixelBuffer, colorBuffer);
        }   
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        if (cmd == null) throw new System.ArgumentNullException("cmd");
        cmd.ReleaseTemporaryRT(pixelBufferID);
        //cmd.ReleaseTemporaryRT(pointBufferID);
    }

}
