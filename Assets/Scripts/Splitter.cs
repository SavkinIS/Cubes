using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    [SerializeField] private Vector2Int _spawnCubesRates =  new Vector2Int(2, 6);
    [SerializeField] private Vector2Int _reduceChanceRates = new Vector2Int(0, 100);
    [SerializeField] private int _reduceValue = 2;
    [SerializeField] private CubesSpawner _cubeSpawner;
    
    private int _chanceToSpawnNewCubes = 100;
    
    public void SplitCube(Cube clickedCube)
    {
        List<Rigidbody> colliders = new List<Rigidbody>();
        
        if (Random.Range(_reduceChanceRates.x, _reduceChanceRates.y + 1) <= _chanceToSpawnNewCubes)
        {
            int count = Random.Range(_spawnCubesRates.x, _spawnCubesRates.y + 1);
            Vector3 size = clickedCube.transform.localScale;
            float smallSize = size.x / _reduceValue;
            clickedCube.SpawnPoints.UpdatePoints();
            
            for (int i = 0; i < count; i++)
            {
                Vector3 newPosition = clickedCube.SpawnPoints.GetPointByIndex(i);
                Cube cube = _cubeSpawner.GetNewCube(newPosition, smallSize);
                colliders.Add(cube.Rigidbody);
            }
            
            _chanceToSpawnNewCubes /= _reduceValue;
        }
        
        clickedCube.Detonator.Explode(colliders);
        _cubeSpawner.DetonatorExploded(clickedCube);
    }
}