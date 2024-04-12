using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizeFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class CustomPassSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public int screenHeight = 144;
        [Header("Rendering")]
        public LayerMask LayerMask = 0;

        // TODO: Try this again when render layers are working with hybrid renderer.
        // [Range(0, 32)]
        // public int RenderLayer = 1;

        public RenderPassEvent RenderPassEvent = RenderPassEvent.AfterRenderingTransparents;

        public SortingCriteria SortingCriteria = SortingCriteria.CommonOpaque;
    }
   // [SerializeField] private OutlineFeature.Settings settings;

    [SerializeField] private CustomPassSettings settings;
    private PixelizePass _pixelizePass;

    public CustomPassSettings Settings { get => settings; set => settings = value; }

    public override void Create()
    {
        if (Settings == null)
        {
            return;
        }
        _pixelizePass = new PixelizePass(settings);
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
#if UNITY_EDITOR
        if (renderingData.cameraData.isSceneViewCamera) return;
#endif
        renderer.EnqueuePass(_pixelizePass);
    }
}


