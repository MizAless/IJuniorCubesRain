using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PoolRequiredWrapper))]
public class ActiveObjectsFromPools : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private PoolRequiredWrapper _poolRequiredWrapper;

    private List<IPoolRequired> _spawners;

    private int _generalCountObjectsFromPools = 0;

    private void Awake()
    {
        _spawners = _poolRequiredWrapper.GetSpawners();
    }

    private void OnEnable()
    {
        _spawners.ForEach(spawner => spawner.ChangedPoolObjectsCount += UpdateView);
    }

    private void OnDisable()
    {
        _spawners.ForEach(spawner => spawner.ChangedPoolObjectsCount -= UpdateView);
    }

    private void UpdateGeneralCount()
    {
        _generalCountObjectsFromPools = 0;

        foreach (var spawner in _spawners)
            _generalCountObjectsFromPools += spawner.GetActiveObjectsCount();
    }

    private void UpdateView()
    {
        UpdateGeneralCount();
        _countText.text = _generalCountObjectsFromPools.ToString();
    }
}
