using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private const string Grounded = "Grounded";
    private const string Speed = "Speed";
    
    [SerializeField] private Animator _animator;
    [SerializeField] private CheckFly _checkFly;
    [SerializeField] private Character _character;
    // Update is called once per frame
    void Update()
    {
        Vector3 localVelocity = _character.transform.InverseTransformVector(_character._Velocity);
        float speed = localVelocity.magnitude / _character._Speed;
        float sign = Mathf.Sign(localVelocity.z);
        
        _animator.SetFloat(Speed, speed * sign);
        _animator.SetBool(Grounded,!_checkFly.IsFly);
    }
}
