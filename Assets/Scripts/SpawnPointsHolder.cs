using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsHolder
{
    private List<Vector3> _points;
    private readonly Transform _transform;

    public SpawnPointsHolder(Transform transform)
    {
        _transform = transform;
    }

    public void UpdatePoints()
    {
        if (_points == null)
            _points = new List<Vector3>();
        else
            _points.Clear();

        Vector3 p = _transform.position;
        float step = _transform.localScale.x / 2;

        _points.Add(new Vector3(p.x - step, p.y, p.z));
        _points.Add(new Vector3(p.x + step, p.y, p.z));
        _points.Add(new Vector3(p.x, p.y, p.z));
        _points.Add(new Vector3(p.x, p.y + step, p.z));
        _points.Add(new Vector3(p.x, p.y, p.z - step));
        _points.Add(new Vector3(p.x, p.y, p.z + step));
    }

    public Vector3 GetPointByIndex(int i)
    {
        if (_points.Count <= i)
            return Vector3.zero;

        return _points[i];
    }
}