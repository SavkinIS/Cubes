using UnityEngine;

public class ColorChanger 
{
    public void UpdateColor(Cube cube)
    {
        cube.MeshRenderer.material.color = Random.ColorHSV();
    }
}