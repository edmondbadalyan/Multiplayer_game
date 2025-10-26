using System;
using UnityEngine;

public class CheckFly : MonoBehaviour
{
    public bool IsFly {get; private set;}
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float coyoteTime = .15f;
    private float _flyTimer = 0;
    private void Update()
    {
        if (Physics.CheckSphere(transform.position, _radius,_layer))
        {
            IsFly = false;
            _flyTimer = 0;
        }
        else
        {
            _flyTimer += Time.deltaTime;
            if (_flyTimer >= coyoteTime)
            {
                IsFly = true;
            }
        }
    }
#if UNITY_EDITOR
private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
#endif
}
