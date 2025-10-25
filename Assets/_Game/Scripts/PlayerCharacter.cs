using UnityEngine;
using UnityEngine.Windows;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Rigidbody rb;
    private float _inputH;
    private float _inputV;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
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
        //var direction = new Vector3(_inputH, 0, _inputV);
        //transform.position += direction * Time.deltaTime * speed;

        Vector3 velocity = (transform.forward * _inputV + transform.right * _inputH).normalized * speed;
        rb.linearVelocity = velocity;

    }

    public void GetMoveInfo(out Vector3 position, out Vector3 velocity)
    {
        position = transform.position;
        velocity = rb.linearVelocity;
    }
}
