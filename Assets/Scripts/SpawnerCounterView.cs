using TMPro;
using UnityEngine;

public class SpawnerCounterView<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Spawner<T> _spawner;

    private string _plural = "'s";
    private string _spawnedObjectName = typeof(T).ToString();
    private string _spawned = "spawned";
    private string _sepatator = ": ";

    private void Awake()
    {
        ChangeView(0);
    }

    private void OnEnable()
    {
        _spawner.Spawned += ChangeView;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= ChangeView;
    }

    private void ChangeView(int newValue)
    {
        string additionalString = _spawnedObjectName + _plural + " " + _spawned + _sepatator;
        _countText.text = additionalString + newValue; 
    }
}
