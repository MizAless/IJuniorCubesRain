using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Destroyer _destroyer;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private BombSpawner _bombSpawner;

    [SerializeField] private Transform _startPoint;

    [SerializeField] private float _maxStartPointOffestX;
    [SerializeField] private float _maxStartPointOffestZ;
    [SerializeField] private float _spawnDelay;

    private void Awake()
    {
        base.Init();
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private void Release(IDestroyable destroyebleCube)
    {
        base.Release(destroyebleCube as Cube);
    }

    protected override void ActionOnGet(Cube cube)
    {
        cube.Init(GetRandomStartPosition());
        cube.OnCollide += _destroyer.DestroyWithDelay;
        cube.OnCollide += _colorChanger.SetRandomColor;
        cube.DestroyPrepared += Release;
        base.ActionOnGet(cube); 
    }

    protected override void ActionOnRelease(Cube cube)
    {
        print("CubeActionOnRelease");
        RemoveAllActions(cube);
        _bombSpawner.Spawn(cube.transform.position);
        base.ActionOnRelease(cube);
    }

    protected override void ActionOnDestroy(Cube cube)
    {
        RemoveAllActions(cube);
        base.ActionOnDestroy(cube);
    }

    private void RemoveAllActions(Cube cube)
    {
        cube.OnCollide -= _destroyer.DestroyWithDelay;
        cube.OnCollide -= _colorChanger.SetRandomColor;
        cube.DestroyPrepared -= Release;
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
            base.Spawn();
        }
    }
}
