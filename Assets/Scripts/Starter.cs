using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private Spawner<Cube> _cubeSpawner;
    [SerializeField] private Spawner<Bomb> _bombSpawner;

    [SerializeField] private CubeSpawnerCounterView _cubeSpawnerCounterView;
    [SerializeField] private BombSpawnerCounterView _bombSpawnerCounterView;

    private void Awake()
    {
        _cubeSpawner.Init();
        _bombSpawner.Init();

        InitView();
    }

    private void InitView()
    {
        _cubeSpawnerCounterView.Init(_cubeSpawner);
        _bombSpawnerCounterView.Init(_bombSpawner);
    }
}
