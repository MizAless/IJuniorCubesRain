using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour 
    where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private T _spawnableObject;

    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<T> _pool;

    private int _createdObjectCount = 0;

    public event Action<int> Spawned;
    public event Action<int> ChangedPoolObjectsCount;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_spawnableObject),
            actionOnGet: (obj) => OnGet(obj),
            actionOnRelease: (obj) => OnRelease(obj),
            actionOnDestroy: (obj) => OnRemoveFromPool(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    public T Spawn()
    {
        return _pool.Get();
    }

    public void Release(T releasedObject)
    {
        _pool.Release(releasedObject);
    }

    protected virtual void OnGet(T getedObject)
    {
        getedObject.gameObject.SetActive(true);
        _createdObjectCount++;
        Spawned?.Invoke(_createdObjectCount);
        ChangedPoolObjectsCount?.Invoke(_pool.CountActive);
    }

    protected virtual void OnRelease(T releasedObject)
    {
        releasedObject.gameObject.SetActive(false);
        ChangedPoolObjectsCount?.Invoke(_pool.CountActive);
    }

    protected virtual void OnRemoveFromPool(T destroyedObject)
    {
        Destroy(destroyedObject.gameObject);
        ChangedPoolObjectsCount?.Invoke(_pool.CountActive);
    }
}
