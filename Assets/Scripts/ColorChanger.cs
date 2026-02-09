using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [Header("Colors")] [SerializeField] private List<Color> _colors = new List<Color>();

    private void OnValidate()
    {   
        if (_meshRenderer == null)
            _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    public void UpdateColor()
    {
        _meshRenderer.material.color = _colors[Random.Range(0, _colors.Count)];
    }
}