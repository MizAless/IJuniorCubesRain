using System;
using UnityEngine;

public class Bomb : MonoBehaviour, ISpawnable, IDestroyable
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private float _startAlpha = 1f;

    public event Action<IDestroyable> DestroyPrepared;
    public event Action<float> ChangedCurrentDestroyProgress;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        ChangedCurrentDestroyProgress += SetAlpha;
    }

    private void OnDisable()
    {
        ChangedCurrentDestroyProgress -= SetAlpha;
    }

    public void Init(Vector3 startPosition)
    {
        transform.position = startPosition;
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        SetAlpha(_startAlpha);
    }

    public void ChangeCurrentDestroyProgress(float progress)
    {
        float reversedProgress = 1 - progress;
        SetAlpha(reversedProgress);
    }

    public void PrepareToDestroy()
    {
        DestroyPrepared?.Invoke(this);
    }

    public void SetAlpha(float value)
    {
        string propertyName = "_Color";
        Color color = _renderer.material.color;

        color.a = value;

        _renderer.material.SetColor(propertyName, color);
    }
}
