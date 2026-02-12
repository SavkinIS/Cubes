using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 500f;
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _multiplier = 2f;
    
    public void Explode(Vector3 explosionPoint, float scaleLevel)
    {
        float explosionRadius = _explosionRadius * Mathf.Pow(_multiplier, scaleLevel);
        float explosionForce = _explosionForce * Mathf.Pow(_multiplier, scaleLevel);

        Collider[] colliders = Physics.OverlapSphere(explosionPoint, explosionRadius);
        
        foreach (Collider hit in colliders)
        {
            if (hit != null && hit.attachedRigidbody != null)
            {
                hit.attachedRigidbody.AddExplosionForce(explosionForce, explosionPoint, explosionRadius);
            }
        }
    }
}