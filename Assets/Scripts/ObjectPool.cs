using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _container;
    private Queue<T> _pool = new();

    public int Count => _pool.Count;

    public ObjectPool(T prefab, Transform container)
    {
        _prefab = prefab;
        _container = container;
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            T instance = Object.Instantiate(_prefab);
            //instance.transform.parent = _container;

            return instance;
        }

        return _pool.Dequeue();
    }

    public void PutObject(T instance)
    {
        _pool.Enqueue(instance);

        //if (instance.transform.parent != _container)
        //    instance.transform.parent = _container;

        instance.gameObject.SetActive(false);
    }
}