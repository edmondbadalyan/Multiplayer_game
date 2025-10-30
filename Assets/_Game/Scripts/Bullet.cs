using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _lifetime = 5f;
   public void Init(Vector3 velocity)
   {
        _rb.linearVelocity = velocity;
        StartCoroutine(DelayDestroy());
   }
    private void Destroy()
    {
        Destroy(gameObject);
    }
    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSecondsRealtime(_lifetime);
        Destroy();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy();
    }
}
