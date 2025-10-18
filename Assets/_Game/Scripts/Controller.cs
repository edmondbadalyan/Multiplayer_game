using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;

    
   private void Update()
   {

        float _inputH = Input.GetAxisRaw("Horizontal");
        float _inputV = Input.GetAxisRaw("Vertical");

        _player.SetInput(_inputH,_inputV);

        SendMove();
   }
    public void SendMove()
    {
        _player.GetMoveInfo(out Vector3 position);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "x",position.x}, { "y",position.z }
        };
        MultiplayerManager.Instance.SendMessage("move",data);
    }
}
