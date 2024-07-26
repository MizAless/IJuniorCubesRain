using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PoolRequiredWrapper))]
public class ActiveObjectsFromPools : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private PoolRequiredWrapper _poolRequiredWrapper;

    private string _additionalString = "Active objects: ";

    private List<IPoolRequired> _spawners;

    private int _generalCountObjectsFromPools = 0;

    private void Awake()
    {
        _spawners = _poolRequiredWrapper.GetSpawners();

        SetText();
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
        SetText();
    }

    private void SetText()
    {
        _countText.text = _additionalString + _generalCountObjectsFromPools.ToString();
    }
}
