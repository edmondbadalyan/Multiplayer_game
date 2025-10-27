using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public float _Speed { get; protected set; } = 2f;
    public Vector3 _Velocity { get; protected set; }
}
