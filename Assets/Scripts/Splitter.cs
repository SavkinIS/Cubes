using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Splitter : MonoBehaviour
{
    [SerializeField] private Vector2Int _spawnCubesRates = new Vector2Int(2, 6);
    [SerializeField] private Vector2Int _reduceChanceRates = new Vector2Int(0, 100);
    [SerializeField] private int _splitReduceStep = 2;
    [SerializeField] private int  _sizeReductionStep = 2;
    [SerializeField] private CubesSpawner _cubeSpawner;
    [SerializeField] private Detonator _detonator;
    [SerializeField] private PlayerInput _playerInput;

    private RaycastLaunch _raycastLauncher;
    private int _chanceToSpawnNewCubes = 100;
    private float _initialSize;

    private void Awake()
    {
        _initialSize = _cubeSpawner.GetInitializeSize();
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
            SplitOrExplodeCube(clickedCube);
        }
    }

    private void SplitOrExplodeCube(Cube clickedCube)
    {
        float currentSize = clickedCube.transform.localScale.x;
        
        if (Random.Range(_reduceChanceRates.x, _reduceChanceRates.y + 1) <= _chanceToSpawnNewCubes)
        {
            int count = Random.Range(_spawnCubesRates.x, _spawnCubesRates.y + 1);
            float smallSize = currentSize / _sizeReductionStep;
            _cubeSpawner.CreateCubes(count, smallSize, clickedCube.transform);
            _chanceToSpawnNewCubes /= _splitReduceStep;
        }
        else
        {
            float scaleLevel = Mathf.Log(_initialSize / currentSize, _sizeReductionStep);
            Vector3 explosionPoint = clickedCube.transform.position;
            _detonator.Explode( explosionPoint, scaleLevel);
        }
        
        _cubeSpawner.DestroyCube(clickedCube);
    }
}