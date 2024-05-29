using System;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
[RequireComponent (typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] Color defaultColor;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private bool _isCubeCollide = false;

    public event Action<Cube> OnCubeCollide;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCubeCollide == false)
        {
            if (collision.gameObject.TryGetComponent<Platform>(out _))
            {
                OnCubeCollide?.Invoke(this);
                _isCubeCollide = true;
            }
        }
    }

    public void Init(Vector3 startPosition)
    {
        transform.position = startPosition;
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        SetColor(defaultColor);
        _isCubeCollide = false;
    }

    public void SetColor(Color color)
    {
        string propertyName = "_Color";

        _renderer.material.SetColor(propertyName, color);
    }
}
