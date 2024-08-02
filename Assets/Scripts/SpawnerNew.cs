//using System;
//using UnityEngine;
//using UnityEngine.Pool;

//public abstract class SpawnerNew : MonoBehaviour
//{
//    [SerializeField] private int _poolCapacity = 5;
//    [SerializeField] private int _poolMaxSize = 5;

//    //private ObjectPool<T> _pool;

//    private int _createdObjectCount = 0;

//    public event Action<int> Spawned;
//    public event Action ChangedPoolObjectsCount;

//    public void Init()
//    {
//        _pool = new ObjectPool<T>(
//            createFunc: () => Instantiate(_spawnableObject),
//            actionOnGet: (obj) => ActionOnGet(obj),
//            actionOnRelease: (obj) => ActionOnRelease(obj),
//            actionOnDestroy: (obj) => ActionOnDestroy(obj),
//            collectionCheck: true,
//            defaultCapacity: _poolCapacity,
//            maxSize: _poolMaxSize
//        );

//        Spawned?.Invoke(_createdObjectCount);
//        ChangedPoolObjectsCount?.Invoke();
//    }

//    public int GetActiveObjectsCount()
//    {
//        return _pool.CountActive;
//    }

//    public virtual T Spawn()
//    {
//        return _pool.Get();
//    }

//    public virtual void Release(T releasedObject)
//    {
//        _pool.Release(releasedObject);
//        ChangedPoolObjectsCount?.Invoke();
//    }

//    protected virtual void ActionOnGet(T getedObject)
//    {
//        getedObject.gameObject.SetActive(true);
//        _createdObjectCount++;
//        Spawned?.Invoke(_createdObjectCount);
//        ChangedPoolObjectsCount?.Invoke();
//    }

//    protected virtual void ActionOnRelease(T releasedObject)
//    {
//        releasedObject.gameObject.SetActive(false);
//        ChangedPoolObjectsCount?.Invoke();
//    }

//    protected virtual void ActionOnDestroy(T destroyedObject)
//    {
//        Destroy(destroyedObject.gameObject);
//    }
//}
