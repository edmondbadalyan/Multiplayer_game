using Colyseus.Schema;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Controller;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCharacter _character;
    [SerializeField] private EnemyGun _gun;
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
    
    private Player _player;

    public void Init(string key, Player player)
    {
        _character.Init(key);
        _player = player;
        _character.SetSpeed(player.speed);
        _character.SetMaxHP(player.maxHP);
        _player.OnChange += OnChange;
    }

    public void Shoot(in ShootInfo shootInfo)
    {
        Vector3 position = new Vector3(shootInfo.pX, shootInfo.pY, shootInfo.pZ);
        Vector3 velocity = new Vector3(shootInfo.dX, shootInfo.dY, shootInfo.dZ);
        _gun.Shoot(position, velocity);
    }
    public void Destroy()
    {
        _player.OnChange -= OnChange;
        Destroy(gameObject);
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
        Vector3 position = _character.TargetPosition;
        Vector3 velocity = _character._Velocity;
        foreach (DataChange dataChange in changes) {

            switch (dataChange.Field)
            {
                case "loss":
                    MultiplayerManager.Instance._lossCounter.SetEnemyLoss((byte)dataChange.Value);
                    break;
                case "currentHP":
                    if((sbyte)dataChange.Value > (sbyte)dataChange.PreviousValue)
                    {
                        _character.RestoreHP((sbyte)dataChange.Value);
                    }
                    break;
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
                case "rX":
                    _character.SetRotateX((float)dataChange.Value);
                    break;
                case "rY":
                    _character.SetRotateY((float)dataChange.Value);
                    break;
                default: Debug.Log("�� �������������� ��������� ����" + dataChange.Field); 
                    break;
            }
        }

        _character.SetMovement(position,velocity, AverageTimeInterval);
    }

    
}
