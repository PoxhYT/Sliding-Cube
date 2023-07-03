using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // Target to follow (your player)
    public float smoothing = 5.0f;  // Speed of camera smoothing
    
    Vector3 offset = new Vector3(0, 1, -5);   // Offset distance between the target and the camera

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
