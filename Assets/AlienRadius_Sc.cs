using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRadius_Sc : MonoBehaviour
{
    public float strength = 10f;
    public float radius = 4f;
    public ForceMode forceMode = ForceMode.Force;
    public AnimationCurve forceDistanceCurce = AnimationCurve.Linear(0, 0, 1, 1);

    private Mesh meshReference;
    List<MagneticBody> magneticBodies = new List<MagneticBody>();

    private void Start()
    {
        meshReference = GetComponent<MeshFilter>().mesh;
    }

    private void FixedUpdate()
    {
        if(magneticBodies.Count > 0) 
        {
            foreach (var magneticBody in magneticBodies)
            {
                float distance = Vector3.Distance(transform.position, magneticBody.transform.position);
                float distanceMulti = forceDistanceCurce.Evaluate(distance) * magneticBody.strengthMultiplyer * strength;
                magneticBody.body.AddForce((transform.position - magneticBody.transform.position).normalized * distanceMulti, forceMode);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.CanGetComponent<MagneticBody>(out var magnetBody))
        {
            if (!magneticBodies.Contains(magnetBody))
            {
                magneticBodies.Add(magnetBody);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null && other.CanGetComponent<MagneticBody>(out var magnetBody))
        {
            if (magneticBodies.Contains(magnetBody))
            {
                magneticBodies.Remove(magnetBody);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireMesh(meshReference,transform.position,Quaternion.Euler(-90,0,0),gameObject.transform.lossyScale);
    }

}
