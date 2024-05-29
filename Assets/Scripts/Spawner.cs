using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Destroyer _destroyer;
    [SerializeField] private ColorChanger _colorChanger;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _startPoint;

    [SerializeField] private float _maxStartPointOffestX;
    [SerializeField] private float _maxStartPointOffestZ;
    [SerializeField] private float _spawnDelay;

    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => ActionOnDestroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );

        _destroyer.DestroyPrepeared += ReleaseCube;
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private void ActionOnGet(Cube cube)
    {
        cube.Init(GetRandomStartPosition());
        cube.OnCubeCollide += _destroyer.DestroyWithDelay;
        cube.OnCubeCollide += _colorChanger.SetRandomColor;
        cube.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Cube cube)
    {
        RemoveAllActions(cube);
        cube.gameObject.SetActive(false);

    }

    private void ActionOnDestroy(Cube cube)
    {
        RemoveAllActions(cube);
        Destroy(cube.gameObject);
    }

    private void RemoveAllActions(Cube cube)
    {
        cube.OnCubeCollide -= _destroyer.DestroyWithDelay;
        cube.OnCubeCollide -= _colorChanger.SetRandomColor;
    }

    private Vector3 GetRandomStartPosition()
    {
        float randomX = UnityEngine.Random.Range(-_maxStartPointOffestX, _maxStartPointOffestX);
        float randomZ = UnityEngine.Random.Range(-_maxStartPointOffestZ, _maxStartPointOffestZ); 

        return new Vector3(randomX, 0, randomZ) + _startPoint.position;
    }

    private IEnumerator Spawning()
    {
        var delay = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            yield return delay;
            GetCube();
        }
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
    }
}
