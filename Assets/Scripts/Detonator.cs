using System;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Collider _collider;

    private float _explosionRadius;
    private float _explosionForce;
    private List<Vector3> _points;

    public event Action<Detonator> Clicked;
    public event Action<Detonator> OnDetonationComplete;

    public Collider Collider => _collider;

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);
    }

    private void OnDestroy()
    {
        Clicked = null;
        OnDetonationComplete = null;
    }

    public Vector3 GetPointByIndex(int index)
    {
        if (_points != null && _points.Count > index)
            return _points[index];

        return Vector3.zero;
    }

    public void Explode(List<Collider> colliders)
    {
        foreach (Collider hit in colliders)
        {
            if (hit != null && hit.attachedRigidbody != null)
            {
                hit.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        OnDetonationComplete?.Invoke(this);
        SetActive(false);
    }

    public void UpdateBeforeSpawn(Vector3 spawnPosition, float size, Color color)
    {
        transform.position = spawnPosition;
        transform.localScale = Vector3.one * size;
        _meshRenderer.material.color = color;
        UpdatePoints();
    }

    public void Init(float explosionRadius, float explosionForce)
    {
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void UpdatePoints()
    {
        _points = new List<Vector3>();

        Vector3 p = transform.position;
        float step = transform.localScale.x / 2;

        _points.Add(new Vector3(p.x - step, p.y, p.z));
        _points.Add(new Vector3(p.x + step, p.y, p.z));
        _points.Add(new Vector3(p.x, p.y, p.z));
        _points.Add(new Vector3(p.x, p.y + step, p.z));
        _points.Add(new Vector3(p.x, p.y, p.z - step));
        _points.Add(new Vector3(p.x, p.y, p.z + step));
    }
}