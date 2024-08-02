using TMPro;
using UnityEngine;

public class SpawnerStatsView<T> : MonoBehaviour
    where T : MonoBehaviour, ISpawnable
{
    private const string Spawned = "Spawned";
    private const string Active = "Active";
    private const string Plural = "'s";
    private const string Sepatator = ":";

    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _activeObjectsCountText;
    [SerializeField] private Spawner<T> _spawner;

    private string _spawnedObjectName = typeof(T).ToString();

    private void Awake()
    {
        UpdateCount(0);
        UpdateActiveObjectsCount(0);
    }

    private void OnDisable()
    {
        _spawner.Spawned -= UpdateCount;
        _spawner.ChangedPoolObjectsCount -= UpdateActiveObjectsCount;
    }

    private void OnEnable()
    {
        _spawner.Spawned += UpdateCount;
        _spawner.ChangedPoolObjectsCount += UpdateActiveObjectsCount;
    }

    private void UpdateCount(int newValue)
    {
        string additionalString = _spawnedObjectName + Plural + " " + Spawned.ToLower() + Sepatator + " ";
        _countText.text = additionalString + newValue;
    }

    private void UpdateActiveObjectsCount(int newValue)
    {
        string additionalString = Active + " " + _spawnedObjectName + Plural + Sepatator + " ";
        _activeObjectsCountText.text = additionalString + newValue;
    }
}
