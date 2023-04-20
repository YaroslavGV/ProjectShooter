using System;
using UnityEngine;
using UnityEngine.Pool;

public enum PoolType
{
    Object,
    Linked
}

[Serializable]
public struct PoolSettings
{
    public int capacity;
    public PoolType type;
    [Tooltip("Used for Object pull type")]
    public int defultCapacity;

    public PoolSettings (int capacity, int defultCapacity) : this()
    {
        this.capacity = capacity;
        this.defultCapacity = defultCapacity;
    }
}

public class ObjectPool<T> : MonoBehaviour where T : Component, IConsumable<T>
{
    public Action<T> OnCreateElement;
    public Action<T> OnDestroyElement;
    public Action<T> OnTakeElement;
    public Action<T> OnReturnElement;

    private PoolSettings _settings;
    private T _template;
    private bool _isInitial = false;
    private IObjectPool<T> _pool;

    public IObjectPool<T> Pool => _pool;
    public int UsedElements { get; private set; }

    protected virtual void OnDestroy () 
    {
        if (_pool != null)
            _pool?.Clear();
    }

    public void Initialize (PoolSettings settings, T template)
    {
        if (_isInitial)
            return;
        
        _settings = settings;
        _template = template;
        if (_settings.type == PoolType.Object)
            _pool = new UnityEngine.Pool.ObjectPool<T>(this.CreatePullObject, this.OnTakeObjectFromPool, this.OnReturnObjectToPool, this.OnDestroyPoolObject, false, _settings.defultCapacity, _settings.capacity);
        else
            _pool = new LinkedPool<T>(CreatePullObject, OnTakeObjectFromPool, OnReturnObjectToPool, OnDestroyPoolObject, false, _settings.capacity);
        _isInitial = true;
    }

    public void ReturnObjectToPool (T element)
    {
        element.transform.SetParent(transform);
        _pool.Release(element);
    }

    private T CreatePullObject ()
    {
        var newObject = Instantiate(_template, transform);
        newObject.SetConsumeCallback(ReturnObjectToPool);
        OnCreateElement?.Invoke(newObject);
        return newObject;
    }

    private void OnDestroyPoolObject (T element)
    {
        if (element != null)
            Destroy(element.gameObject);
        OnDestroyElement?.Invoke(element);
    }

    private void OnTakeObjectFromPool (T element)
    {
        element.gameObject.SetActive(true);
        UsedElements++;
        OnTakeElement?.Invoke(element);
    }

    public void OnReturnObjectToPool (T element)
    {
        element.gameObject.SetActive(false);
        UsedElements--;
        OnReturnElement?.Invoke(element);
    }
}
