using System.Collections.Generic;
using UnityEngine;

public class PoolRequiredWrapper : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _spawners;

    public List<IPoolRequired> GetSpawners()
    {
        List<IPoolRequired> spawners = new();

        foreach (var spawner in _spawners)
        {
            spawners.Add((IPoolRequired)spawner);
        }

        return spawners;
    }
}
