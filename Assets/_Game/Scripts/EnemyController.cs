using Colyseus.Schema;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCharacter _character;
    private List<float> _receiveTimeInterval = new List<float> { 0, 0, 0, 0, 0 };

    private float _lastReceiveTime = 0;

    private float AverageTimeInterval
    {
        get { 
            int count = _receiveTimeInterval.Count;
            float sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += _receiveTimeInterval[i];
            }
            return sum / count;
        }
    }
    private void SaveReceiveTime()
    {
        float interval = Time.time - _lastReceiveTime;
        _lastReceiveTime = Time.time;

        _receiveTimeInterval.Add(interval);
        _receiveTimeInterval.RemoveAt(0);
    }
    internal void OnChange(List<DataChange> changes)
    {
        SaveReceiveTime();
        Vector3 position = transform.position;
        Vector3 velocity = Vector3.zero;
        foreach (DataChange dataChange in changes) {

            switch (dataChange.Field)
            {
                case "pX":
                    position.x = (float)dataChange.Value;
                    break;
                case "pY":
                    position.y = (float)dataChange.Value;
                    break;
                case "pZ":
                    position.z = (float)dataChange.Value;
                    break;
                case "vX":
                    velocity.x = (float)dataChange.Value;
                    break;
                case "vY":
                    velocity.y = (float)dataChange.Value;
                    break;
                case "vZ":
                    velocity.z = (float)dataChange.Value;
                    break;
                default: Debug.Log("�� �������������� ��������� ����" + dataChange.Field); 
                    break;
            }
        }

        _character.SetMovement(position,velocity, AverageTimeInterval);
    }

    
}
