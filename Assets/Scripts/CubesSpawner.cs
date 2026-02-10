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
        _colorChanger =  new ColorChanger();
        int spawnRate = Random.Range(_startCubesCountRate.x, _startCubesCountRate.y + 1);

        for (int i = 0; i < spawnRate; i++)
        {
            var cube = InstatiateCube();
            Vector3 spawnPosition = Random.insideUnitSphere * _startSpawnRange;
            spawnPosition.y = cube.transform.localScale.y / 2;
            SpawnCube(cube, spawnPosition, cube.transform.localScale.x);
        }
    }

    private Cube InstatiateCube()
    {
        Cube cube = Instantiate(_cubePrefab);
        _colorChanger.UpdateColor(cube);
        return cube;
    }

    public void DestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);
    }

    public List<Rigidbody> CreateCubes(int count, float size, Transform transform)
    {
        List<Rigidbody> colliders = new List<Rigidbody>();
        List<Vector3> points = GetSpawnPoints(transform);
        
        for (int i = 0; i < count; i++)
        {
            Vector3 newPosition = points[i];
            Cube cube = GetNewCube(newPosition, size);
            colliders.Add(cube.Rigidbody);
        }
        
        return colliders;
    }
    
    private Cube GetNewCube( Vector3 spawnPosition, float size)
    {
        Cube cube = InstatiateCube();
        SpawnCube(cube, spawnPosition, size);
        return cube;
    }
    
    private void SpawnCube(Cube cube, Vector3 spawnPosition, float size)
    {
        cube.UpdateBeforeSpawn(spawnPosition, size);
    }
    
    private List<Vector3> GetSpawnPoints(Transform cubeTransform)
    {
        float reducePosition = 2f;
        List<Vector3> points = new List<Vector3>();

        Vector3 p = cubeTransform.position;
        float step = cubeTransform.localScale.x / reducePosition;

        points.Add(new Vector3(p.x - step, p.y, p.z));
        points.Add(new Vector3(p.x + step, p.y, p.z));
        points.Add(new Vector3(p.x, p.y, p.z));
        points.Add(new Vector3(p.x, p.y + step, p.z));
        points.Add(new Vector3(p.x, p.y, p.z - step));
        points.Add(new Vector3(p.x, p.y, p.z + step));
        
        return points;
    }
}
