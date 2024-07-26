using UnityEngine;
using UnityEngine.EventSystems;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private Destroyer _destroyer;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private ColorChanger _colorChanger;

    private void Awake()
    {
        base.Init();
    }

    private void Release(IDestroyable destroyebleBomb)
    {
        base.Release(destroyebleBomb as Bomb);
    }

    protected override void ActionOnRelease(Bomb bomb)
    {
        print("BombActionOnRelease");
        _exploder.Explode(bomb.transform.position);
        bomb.DestroyPrepared -= Release;
        base.ActionOnRelease(bomb);
    }

    public void Spawn(Vector3 position)
    {
        Bomb bomb = base.Spawn();
        bomb.Init(position);
        bomb.DestroyPrepared += Release;
        _destroyer.DestroyWithDelay(bomb);
    }
}
