using System;
using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float minSecondsToDestroy = 2f;
    [SerializeField] private float maxSecondsToDestroy = 5f;

    public event Action<Cube> DestroyPrepeared;

    private void OnValidate()
    {
        if (minSecondsToDestroy > maxSecondsToDestroy)
            minSecondsToDestroy = maxSecondsToDestroy - 1f;
    }

    public void DestroyWithDelay(Cube cube)
    {
        float secondsToDestroy = UnityEngine.Random.Range(minSecondsToDestroy, maxSecondsToDestroy);
        
        StartCoroutine(PrepareDestroy(cube, secondsToDestroy));
    }

    private IEnumerator PrepareDestroy(Cube cube, float secondsToDestroy)
    {
        yield return new WaitForSeconds(secondsToDestroy);

        DestroyPrepeared?.Invoke(cube);
    }
}
