using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionShoes_SC : MonoBehaviour
{
    [SerializeField] PoolShoes poolSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.CanGetComponent<MagneticBody>(out var magnetBody))
        {
            poolSystem.ReturnShoes(other.gameObject);
        }
    }
}
