using System;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private PlayerGun _gun;
    [SerializeField] float mouseSensitivity = 2f;


    private void Update()
    {

        float _inputH = Input.GetAxisRaw("Horizontal");
        float _inputV = Input.GetAxisRaw("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool space = Input.GetKeyDown(KeyCode.Space);
        bool isShoot = Input.GetMouseButton(0);


        _player.SetInput(_inputH, _inputV, mouseX * mouseSensitivity);
        _player.RotateX(-mouseY * mouseSensitivity);
        if (space) _player.Jump();
        if (isShoot && _gun.TryShoot(out ShootInfo shootInfo))
        {
            SendShoot(ref shootInfo);
        }
        SendMove();
    }
    public void SendMove()
    {
        _player.GetMoveInfo(out Vector3 position, out Vector3 velocity, out float RotateX, out float RotateY);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "pX",position.x },
            { "pY",position.y },
            { "pZ",position.z },
            { "vX",velocity.x },
            { "vY",velocity.y },
            { "vZ",velocity.z },
            { "rX",RotateX },
            { "rY",RotateY }
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }

    private void SendShoot(ref ShootInfo shootInfo)
    {
        shootInfo.key = MultiplayerManager.Instance.GetSesssionId();
        string json = JsonUtility.ToJson(shootInfo);
        MultiplayerManager.Instance.SendMessage("shoot",json);
    }


    [Serializable]
    public struct ShootInfo
    {
        public string key;
        public float dX;
        public float dY;
        public float dZ;

        public float pX;
        public float pY;
        public float pZ;
    }
}
