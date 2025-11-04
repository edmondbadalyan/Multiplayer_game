using System;
using UnityEngine;

public class LootAtCamera : MonoBehaviour
{
    private Transform cameraTransform;


    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(cameraTransform);
    }
}
