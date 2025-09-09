using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component, IPooling
{
    private readonly List<T> _pool;
    private readonly T _prefab;
    private readonly Transform _parent;

    public ObjectPool(T prefab, Transform parent, int initialSize = 3)
    {
        _prefab = prefab;
        _parent = parent;
        _pool = new List<T>(10);
        for (int i = 0; i < initialSize; i++)
            NewItemInstantiate();
    }

    public T GetFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (_pool[i].gameObject.activeInHierarchy)
                continue;

            return _pool[i];
        }

        T newItem = NewItemInstantiate();
        return newItem;
    }

    public void ReturnToPool(T item) => item.gameObject.SetActive(false);

    private T NewItemInstantiate()
    {
        var newItem = Object.Instantiate(_prefab, _parent);
        newItem.gameObject.SetActive(false);
        _pool.Add(newItem);
        return newItem;
    }
}
