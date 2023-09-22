using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 

    public float smoothSpeed = 0.5f; 

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position; 
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset; 
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed); 

        transform.position = smoothPosition; 
    }
}
