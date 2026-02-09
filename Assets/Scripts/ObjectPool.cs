using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private int _poolSize = 50;
    private Queue<T> _objectPool = new Queue<T>();
    private Transform _parent;
    private readonly Action<T> _initializeAction;

    public ObjectPool(Transform parent, T prefab, Action<T> initializeAction)
    {
        _initializeAction = initializeAction;
        _prefab = prefab;
        _parent  = parent;
        InitializePool();
    }
    
    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            CreateNewObject();
        }
    }
    
    private void CreateNewObject()
    {
        T obj = UnityEngine.Object.Instantiate(_prefab, _parent);
        obj.gameObject.SetActive(false);
        _objectPool.Enqueue(obj);
        _initializeAction?.Invoke(obj);
    }
    
    public T GetObject()
    {
        if (_objectPool.Count == 0)
        {
            CreateNewObject();
        }
        
        T obj = _objectPool.Dequeue();
        return obj;
    }
    
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _objectPool.Enqueue(obj);
    }

    public IEnumerable<T> GetAllItems()
    {
        return _objectPool.ToArray();
    }
}