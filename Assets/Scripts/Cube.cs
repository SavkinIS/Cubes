using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer),  typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _meshRenderer;

    public Rigidbody Rigidbody => _rigidbody;
    public MeshRenderer MeshRenderer => _meshRenderer;

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        
        if (_meshRenderer == null)
            _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void UpdateBeforeSpawn(Vector3 spawnPosition, float size)
    {
        transform.position = spawnPosition;
        transform.localScale = Vector3.one * size;
    }
}