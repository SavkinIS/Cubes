using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    public event Action<Vector2> MouseClicked;

    private const int Click = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(Click))
        {
            MouseClicked?.Invoke(Input.mousePosition);
        }
    }
}