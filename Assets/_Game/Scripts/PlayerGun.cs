using System;
using UnityEngine;
using static Controller;

public class PlayerGun : Gun
{
    
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootDelay;
    private float _lastShootTime;
    

    public bool TryShoot(out ShootInfo info)
    {
        info = new ShootInfo();
        if (Time.time - _lastShootTime < _shootDelay) return false;

        Vector3 position = _bulletPoint.position;
        Vector3 velocity = _bulletPoint.forward * _bulletSpeed;
        _lastShootTime = Time.time;

        Bullet check = Instantiate(_bulletPrefab, position, _bulletPoint.rotation);
        check.Init(velocity);
        shoot?.Invoke();
        Debug.Log($"{check}");
        

        info.pX = position.x;
        info.pY = position.y;
        info.pZ = position.z;
        info.dX = velocity.x;
        info.dY = velocity.y;
        info.dZ = velocity.z;

        return true;
    }
}
