using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float restartDelay = 3f;
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private PlayerGun _gun;
    [SerializeField] float mouseSensitivity = 2f;
    private bool _hold = false;
    private bool _hideCursor;


    private void Start()
    {
        _hideCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _hideCursor = !_hideCursor;
            Cursor.lockState = _hideCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }
        if (_hold) return;

        float _inputH = Input.GetAxisRaw("Horizontal");
        float _inputV = Input.GetAxisRaw("Vertical");

        float mouseX = 0;
        float mouseY = 0;
        bool isShoot = false;

        if (_hideCursor)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            isShoot = Input.GetMouseButton(0);
        }
        
        bool space = Input.GetKeyDown(KeyCode.Space);
        
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

    public void Restart(int spawnIndex)
    {
        MultiplayerManager.Instance._spawnPoints.GetPoint(spawnIndex, out Vector3 position,out Vector3 rotation);
        StartCoroutine(HOLD());
        _player.transform.position = position;
        rotation.x = 0;
        rotation.z = 0;
        _player.transform.eulerAngles = rotation;
        _player.SetInput(0, 0, 0);

        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "pX",position.x },
            { "pY",position.y },
            { "pZ",position.z },
            { "vX",0 },
            { "vY",0 },
            { "vZ",0 },
            { "rX",0 },
            { "rY",rotation.y }
        };
        MultiplayerManager.Instance.SendMessage("move", data);
    }
    private IEnumerator HOLD()
    {
        _hold = true;
        yield return new WaitForSecondsRealtime(restartDelay);
        _hold = false;
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
