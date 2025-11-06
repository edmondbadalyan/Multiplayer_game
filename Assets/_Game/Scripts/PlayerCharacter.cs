
using Colyseus.Schema;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class PlayerCharacter : Character
{
    [SerializeField] private Health _health;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _cameraPoint;
    [SerializeField] private float _minHeadAngle = -90;
    [SerializeField] private float _maxHeadAngle = 90;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private CheckFly checkFly;
    private Rigidbody _rb;
    private float _inputH;
    private float _inputV;
    private float _rotateY;
    private float _currentRotationX;
    private float _jumpDelay = .15f;
    private float  jumpTimer;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _Speed = speed;
    }

    private void Start()
    {
       
        
            Transform camera = Camera.main.transform;
            camera.parent = _cameraPoint;
            camera.localPosition = Vector3.zero;
            camera.localRotation = Quaternion.identity;
         

        _health.SetMax(MaxHealth);
        _health.SetCurrent(MaxHealth);
    }

    private void FixedUpdate()
    {
        Move();
        RotateY();
    }

    public void SetInput(float h, float v, float rotateY)
    {
        _inputH = h;
        _inputV = v;
        _rotateY += rotateY;
    }

    public void Move()
    {
        //var direction = new Vector3(_inputH, 0, _inputV);
        //transform.position += direction * Time.deltaTime * speed;

        Vector3 velocity = (transform.forward * _inputV + transform.right * _inputH).normalized * speed;
        velocity.y = _rb.linearVelocity.y;
        _Velocity = velocity;
        _rb.linearVelocity = _Velocity;

    }

    public void RotateX(float value)
    {
        _currentRotationX = Mathf.Clamp(_currentRotationX + value, _minHeadAngle, _maxHeadAngle);
        //_head.localEulerAngles = new Vector3(_currentRotationX, 0, 0);
        _head.localRotation = Quaternion.Euler(_currentRotationX, 0, 0);
        //_head.Rotate(value,0,0);
    }
    private void RotateY()
    {
        _rb.angularVelocity = new Vector3(0,_rotateY,0);
        _rotateY = 0;
    }
    public void GetMoveInfo(out Vector3 position, out Vector3 velocity,out float RotateX, out float RotateY)
    {
        position = transform.position;
        velocity = _rb.linearVelocity;
        
        RotateX = _head.localEulerAngles.x;
        RotateY = transform.eulerAngles.y;
    }

    // private bool isFly = true;
    // private void OnCollisionStay(Collision other)
    // {
    //     var contact = other.contacts;
    //     foreach (var contactPoint in contact)
    //     {
    //         Debug.Log(contactPoint.normal.y);
    //         if (contactPoint.normal.y > .45f)
    //         {
    //             isFly = false;
    //         }
    //     }
    // }
    //
    // private void OnCollisionExit(Collision other)
    // {
    //     isFly = true;
    // }

    public void Jump()
    {
        if (checkFly.IsFly) return;
        if (Time.time - jumpTimer < _jumpDelay) return;

        jumpTimer = Time.time; 
        _rb.AddForce(0,jumpForce,0,ForceMode.VelocityChange);
    }

    internal void OnChange(List<DataChange> changes)
    {
        foreach (DataChange dataChange in changes)
        {

            switch (dataChange.Field)
            {
                case "loss":
                    MultiplayerManager.Instance._lossCounter.SetPlayerLoss((byte)dataChange.Value);
                    break;
                case "currentHP":
                    _health.SetCurrent((sbyte)dataChange.Value);
                    break;
                default:
                    Debug.Log("�� �������������� ��������� ����" + dataChange.Field);
                    break;
            }
        }
    }
}
