using System;
public interface IPoolRequired
{
    public event Action ChangedPoolObjectsCount;

    public int GetActiveObjectsCount();
}
