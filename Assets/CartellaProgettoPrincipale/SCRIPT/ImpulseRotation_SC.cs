using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseRotation_SC : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(Random.Range(0.1f,2), Random.Range(0.1f, 2), Random.Range(0.1f, 2)), ForceMode.Impulse);
    }
}
