using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CubesController : MonoBehaviour
{
    [SerializeField] private Detonator _detonatorPrefab;
    [SerializeField] private Vector2Int _startCubesCountRate;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private Vector2 _spawnPosition;
    [SerializeField] private Vector2Int _spawnCubesRates;
    [SerializeField] private Vector2 _reduceChanceRates = new Vector2Int(0, 100);
    [SerializeField] private int _reduceValue = 2;
    [Header("Colors")] [SerializeField] private List<Color> _colors = new List<Color>();

    private readonly List<Detonator> _activeDetonators = new List<Detonator>();
    private readonly float _startSpawnRange = 10;
    private ObjectPool<Detonator> _detonatorsPool;
    private int _chanceToSpawnNewCubes = 100;
    private bool _isUnsubscribedble;

    private void Awake()
    {
        _detonatorsPool = new ObjectPool<Detonator>(transform, _detonatorPrefab);

        foreach (var detonator in _detonatorsPool.GetAllItems())
        {
            detonator.Init(_explosionRadius, _explosionForce);
        }

        int spawnRate = Random.Range(_startCubesCountRate.x, _startCubesCountRate.y + 1);

        for (int i = 0; i < spawnRate; i++)
        {
            Color color = _colors[Random.Range(0, _colors.Count)];
            Detonator detonator = _detonatorsPool.GetObject();
            Vector3 spawnPosition = Random.insideUnitSphere * _startSpawnRange;
            spawnPosition.y = detonator.transform.localScale.y / 2;
            SpawnDetonator(detonator, spawnPosition, detonator.transform.localScale.x, color);
        }
    }

    private void OnEnable()
    {
        if (_isUnsubscribedble)
        {
            foreach (var detonator in _activeDetonators)
                Subscribe(detonator);
        }
    }

    private void OnDisable()
    {
        foreach (var detonator in _activeDetonators)
            Unsubscribe(detonator);
        
        _isUnsubscribedble = true;
    }

    private void SpawnDetonator(Detonator detonator, Vector3 spawnPosition, float size, Color color)
    {
        detonator.UpdateBeforeSpawn(spawnPosition, size, color);
        Subscribe(detonator);
        _activeDetonators.Add(detonator);
        detonator.SetActive(true);
    }

    private void Subscribe(Detonator detonator)
    {
        detonator.Clicked += MouseClicked;
        detonator.OnDetonationComplete += DetonatorExploded;
    }

    private void DetonatorExploded(Detonator detonator)
    {
        Unsubscribe(detonator);
        _activeDetonators.Remove(detonator);
        _detonatorsPool.ReturnObject(detonator);
    }

    private void Unsubscribe(Detonator detonator)
    {
        detonator.Clicked -= MouseClicked;
        detonator.OnDetonationComplete -= DetonatorExploded;
    }

    private void MouseClicked(Detonator detonated)
    {
        List<Collider> colliders = new List<Collider>();
        
        if (Random.Range(_reduceChanceRates.x, _reduceChanceRates.y + 1) <= _chanceToSpawnNewCubes)
        {
            int count = Random.Range(_spawnCubesRates.x, _spawnCubesRates.y + 1);
            Vector3 size = detonated.transform.localScale;

            float smallSize = size.x / _reduceValue;

            for (int i = 0; i < count; i++)
            {
                Color color = _colors[Random.Range(0, _colors.Count)];
                Vector3 newPosition = detonated.GetPointByIndex(i);
                Detonator detonator = _detonatorsPool.GetObject();
                SpawnDetonator(detonator, newPosition, smallSize, color);
                colliders.Add(detonator.Collider);
            }
        }
        
        detonated.Explode(colliders);
        
        _chanceToSpawnNewCubes /= _reduceValue;
    }
}