using TMPro;
using UnityEngine;

public class SpawnerView<T> : MonoBehaviour
    where T : MonoBehaviour, ISpawnable
{
    private const string Spawned = nameof(Spawned);
    private const string AdditionalString = "Active objects: ";

    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _activeObjectsCountText;
    private Spawner<T> _spawner;

    private string _plural = "'s";
    private string _spawnedObjectName = typeof(T).ToString();
    private string _sepatator = ": ";

    private void Awake()
    {
        UpdateCount(0);
    }

    private void OnDestroy()
    {
        _spawner.Spawned -= UpdateCount;
        _spawner.ChangedPoolObjectsCount -= UpdateActiveObjectsCount;
    }

    public void Init(Spawner<T> spawner)
    {
        _spawner = spawner;

        _spawner.Spawned += UpdateCount;
        _spawner.ChangedPoolObjectsCount += UpdateActiveObjectsCount;
    }

    private void UpdateCount(int newValue)
    {
        string additionalString = _spawnedObjectName + _plural + " " + Spawned.ToLower() + _sepatator;
        _countText.text = additionalString + newValue;
    }

    private void UpdateActiveObjectsCount(int newValue)
    {
        _activeObjectsCountText.text = AdditionalString + newValue;
    }
}
