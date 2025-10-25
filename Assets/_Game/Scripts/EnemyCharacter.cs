using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    private Vector3 _targetPosition;
    private float _velocityMagnitude;


    private void Update()
    {
        if (_velocityMagnitude > .1f)
        {
            float maxDistance = _velocityMagnitude * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, maxDistance);
        }
        else { transform.position = _targetPosition; }

    }

    public void SetMovement(in Vector3 position,in Vector3 velocity,in float averageTimeInterval)
    {
        _targetPosition = position + (velocity * averageTimeInterval);
        _velocityMagnitude = velocity.magnitude;
    }
}
