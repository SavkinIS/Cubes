using System;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    
    public event Action<Detonator> OnDetonationComplete;

    private void OnDestroy()
    {
        OnDetonationComplete = null;
    }
    
    public void Explode(List<Rigidbody> rigidbodies)
    {
        foreach (Rigidbody hit in rigidbodies)
        {
            if (hit != null &&rigidbodies != null)
            {
                hit.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        OnDetonationComplete?.Invoke(this);
    }
}