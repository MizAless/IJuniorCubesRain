using System;

public interface IDestroyable
{
    public event Action<IDestroyable> DestroyPrepared;
    public event Action<float> ChangedCurrentDestroyProgress;

    public void PrepareToDestroy();
    public void ChangeCurrentDestroyProgress(float progress);
}