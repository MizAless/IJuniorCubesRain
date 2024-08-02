using UnityEngine;

public class BombFactory : MonoBehaviour
{
    [SerializeField] private Bomb _prefab;

    public Bomb Create(Vector3 at, Quaternion quaternion = default, Transform parent = null)
    {
        return Instantiate(_prefab, at, quaternion, parent);
    }
}
