using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float minSecondsToDestroy = 2f;
    [SerializeField] private float maxSecondsToDestroy = 5f;

    private void OnValidate()
    {
        if (minSecondsToDestroy > maxSecondsToDestroy)
            minSecondsToDestroy = maxSecondsToDestroy - 1f;
    }

    public void DestroyWithDelay(IDestroyable destroyableObj)
    {
        float secondsToDestroy = UnityEngine.Random.Range(minSecondsToDestroy, maxSecondsToDestroy);
        
        StartCoroutine(PrepareDestroy(destroyableObj, secondsToDestroy));
    }

    private IEnumerator PrepareDestroy(IDestroyable destroyableObj, float secondsToDestroy)
    {
        float progress = 0;
        float expiredTime = 0;
        float maxProgress = 1;

        var delay = new WaitForFixedUpdate();

        while (progress < maxProgress)
        {
            expiredTime += Time.fixedDeltaTime;
            progress = expiredTime / secondsToDestroy;
            destroyableObj.ChangeCurrentDestroyProgress(progress);
            yield return delay;
        }

        destroyableObj.PrepareToDestroy();
    }
}
