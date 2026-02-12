using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Vector2Int _startCubesCountRate;

    private readonly float _startSpawnRange = 10;
    private ColorChanger _colorChanger;

    public void Awake()
    {
        _colorChanger = new ColorChanger();
        int spawnRate = Random.Range(_startCubesCountRate.x, _startCubesCountRate.y + 1);

        for (int i = 0; i < spawnRate; i++)
        {
            var cube = InstatiateCube();
            Vector3 spawnPosition = Random.insideUnitSphere * _startSpawnRange;
            spawnPosition.y = cube.transform.localScale.y / 2;
            SpawnCube(cube, spawnPosition, cube.transform.localScale.x);
        }
    }

    public void DestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);
    }

    public List<Rigidbody> CreateCubes(int count, float size, Transform calledCubeTransform)
    {
        List<Rigidbody> colliders = new List<Rigidbody>();
        Vector3 newPosition = calledCubeTransform.position;

        for (int i = 0; i < count; i++)
        {
            Cube cube = GetNewCube(newPosition, size);
            colliders.Add(cube.Rigidbody);
        }

        return colliders;
    }

    private Cube GetNewCube(Vector3 spawnPosition, float size)
    {
        Cube cube = InstatiateCube();
        SpawnCube(cube, spawnPosition, size);
        return cube;
    }

    private void SpawnCube(Cube cube, Vector3 spawnPosition, float size)
    {
        cube.UpdateBeforeSpawn(spawnPosition, size);
    }

    private Cube InstatiateCube()
    {
        Cube cube = Instantiate(_cubePrefab);
        _colorChanger.UpdateColor(cube);
        return cube;
    }
}