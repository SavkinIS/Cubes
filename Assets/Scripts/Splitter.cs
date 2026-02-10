using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Splitter : MonoBehaviour
{
    [SerializeField] private Vector2Int _spawnCubesRates = new Vector2Int(2, 6);
    [SerializeField] private Vector2Int _reduceChanceRates = new Vector2Int(0, 100);
    [SerializeField] private int _reduceValue = 2;
    [SerializeField] private CubesSpawner _cubeSpawner;
    [SerializeField] private Detonator _detonator;
    [SerializeField] private PlayerInput _playerInput;

    private RaycastLaunch _raycastLauncher;
    private int _chanceToSpawnNewCubes = 100;

    private void Awake()
    {
        _raycastLauncher = new RaycastLaunch();
    }

    private void OnEnable()
    {
        _playerInput.MouseClicked += MouseClicked;
    }

    private void OnDisable()
    {
        _playerInput.MouseClicked -= MouseClicked;
    }

    private void MouseClicked(Vector2 mousePosition)
    {
        if (_raycastLauncher.TryGetClickedObject(mousePosition, out Cube clickedCube))
        {
            SplitCube(clickedCube);
        }
    }

    private void SplitCube(Cube clickedCube)
    {
        if (Random.Range(_reduceChanceRates.x, _reduceChanceRates.y + 1) <= _chanceToSpawnNewCubes)
        {
            List<Rigidbody> colliders = new List<Rigidbody>();
            int count = Random.Range(_spawnCubesRates.x, _spawnCubesRates.y + 1);
            count = 6;
            
            Vector3 size = clickedCube.transform.localScale;
            float smallSize = size.x / _reduceValue;
            colliders = _cubeSpawner.CreateCubes(count, smallSize, clickedCube.transform);
            _chanceToSpawnNewCubes /= _reduceValue;
            _detonator.Explode(colliders, clickedCube.transform);
        }
        
        _cubeSpawner.DestroyCube(clickedCube);
    }
}