using System;
public interface IPoolRequired
{
    public event Action<int> ChangedPoolObjectsCount;
}
