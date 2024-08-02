using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private Destroyer _destroyer;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private ColorChanger _colorChanger;

    private void Release(IDestroyable destroyebleBomb)
    {
        base.Release(destroyebleBomb as Bomb);
    }

    protected override void OnRelease(Bomb bomb)
    {
        _exploder.Explode(bomb.transform.position);
        bomb.DestroyPrepared -= Release;
        base.OnRelease(bomb);
    }

    public void Spawn(Vector3 position)
    {
        Bomb bomb = base.Spawn();
        bomb.Init(position);
        bomb.DestroyPrepared += Release;
        _destroyer.DestroyWithDelay(bomb);
    }
}
