using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public float _Speed { get; protected set; } = 2f;
    [field: SerializeField] public int MaxHealth { get; protected set; } = 10;
    public Vector3 _Velocity { get; protected set; }
}
