using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseRotation_SC : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.AddTorque(new Vector3(Random.Range(0.1f,2), Random.Range(0.1f, 2), Random.Range(0.1f, 2)), ForceMode.Impulse);
    }
}
