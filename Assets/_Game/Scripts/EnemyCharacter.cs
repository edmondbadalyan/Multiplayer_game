using System;
using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private Transform _head;
    public Vector3 TargetPosition { get; private set; } = Vector3.zero;
    private float _velocityMagnitude = 0;

    private void Start()
    {
        TargetPosition = transform.position;
    }

    public void SetSpeed(float speed) => _Speed = speed;
    private void Update()
    {
        if (_velocityMagnitude > .1f)
        {
            float maxDistance = _velocityMagnitude * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, maxDistance);
        }
        else { transform.position = TargetPosition; }

    }

    public void SetMovement(in Vector3 position,in Vector3 velocity,in float averageTimeInterval)
    {
        TargetPosition = position + (velocity * averageTimeInterval);
        _Velocity = velocity;
        _velocityMagnitude = velocity.magnitude;
    }

    public void SetRotateX(float value)
    {
        _head.localEulerAngles = new Vector3(value, 0, 0);
    }
    public void SetRotateY(float value)
    {
        transform.localEulerAngles = new Vector3(0, value, 0);
    }
}
