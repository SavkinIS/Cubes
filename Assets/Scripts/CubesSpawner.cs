using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Vector2Int _startCubesCountRate;
    
    private readonly List<Cube> _activeCubes = new List<Cube>();
    private readonly float _startSpawnRange = 10;
    private ObjectPool<Cube> _cubePool;

    public void Awake()
    {
        _cubePool = new ObjectPool<Cube>(transform, cubePrefab, InitializeCubeAction);
        int spawnRate = Random.Range(_startCubesCountRate.x, _startCubesCountRate.y + 1);

        for (int i = 0; i < spawnRate; i++)
        {
            Cube cube = _cubePool.GetObject();
            Vector3 spawnPosition = Random.insideUnitSphere * _startSpawnRange;
            spawnPosition.y = cube.transform.localScale.y / 2;
            SpawnCube(cube, spawnPosition, cube.transform.localScale.x);
            cube.SetActive(true);
        }
    }

    public void DetonatorExploded(Cube cube)
    {
        cube.SetActive(false);
        _activeCubes.Remove(cube);
        _cubePool.ReturnObject(cube);
    }

    public Cube GetNewCube( Vector3 spawnPosition, float size)
    {
        Cube cube = _cubePool.GetObject();
        SpawnCube(cube, spawnPosition, size);
        cube.SetActive(true);
        return cube;
    }
    
    private void SpawnCube(Cube cube, Vector3 spawnPosition, float size)
    {
        cube.UpdateBeforeSpawn(spawnPosition, size);
        _activeCubes.Add(cube);
    }
    
    private void InitializeCubeAction(Cube cube)
    {
        cube.Init();
    }
}
