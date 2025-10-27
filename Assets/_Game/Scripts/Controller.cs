using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] float mouseSensitivity = 2f;

    
   private void Update()
   {

        float _inputH = Input.GetAxisRaw("Horizontal");
        float _inputV = Input.GetAxisRaw("Vertical");
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool space = Input.GetKeyDown(KeyCode.Space);

        _player.SetInput(_inputH,_inputV, mouseX *  mouseSensitivity);
        _player.RotateX(-mouseY * mouseSensitivity);
        if (space) _player.Jump();

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
        MultiplayerManager.Instance.SendMessage("move",data);
    }
}
