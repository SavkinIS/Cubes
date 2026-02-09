using System;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Splitter _splitter;
    [SerializeField] private PlayerInput _playerInput;
    private RaycastLaunch _raycastLauncher;

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
            _splitter.SplitCube(clickedCube);
        }
    }
}