using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Detonator), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Detonator _detonator;

    private SpawnPointsHolder _spawnPointsHolder;

    public Rigidbody Rigidbody => _rigidbody;
    public Detonator Detonator => _detonator;
    public SpawnPointsHolder SpawnPoints => _spawnPointsHolder;

    private void OnValidate()
    {
        if (_colorChanger == null)
            _colorChanger = GetComponent<ColorChanger>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        _spawnPointsHolder = new SpawnPointsHolder(transform);
    }

    public void UpdateBeforeSpawn(Vector3 spawnPosition, float size)
    {
        transform.position = spawnPosition;
        transform.localScale = Vector3.one * size;
        _colorChanger.UpdateColor();
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}