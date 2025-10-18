using UnityEngine;
using UnityEngine.Windows;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private float _inputH;
    private float _inputV;

    private void Update()
    {
        Move();
    }

    public void SetInput(float h, float v)
    {
        _inputH = h;
        _inputV = v;
    }

    public void Move()
    {
        var direction = new Vector3(_inputH, 0, _inputV);
        transform.position += direction * Time.deltaTime * speed;

    }

    public void GetMoveInfo(out Vector3 position)
    {
        position = transform.position;
    }
}
