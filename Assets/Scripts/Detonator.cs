using System;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 500f;
    [SerializeField] private float _explosionForce = 500f;
    
    public void Explode(List<Rigidbody> rigidbodies, Vector3 center)
    {
        foreach (Rigidbody hit in rigidbodies)
        {
            if (hit != null && rigidbodies != null)
            {
                hit.AddExplosionForce(_explosionRadius, center, _explosionRadius);
            }
        }
    }
}