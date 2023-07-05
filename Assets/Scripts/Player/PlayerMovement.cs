using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(500 * Time.deltaTime, 0, 0);
        float horizontalInput = Input.GetAxis("Horizontal") * -1;
        
        if (Mathf.Abs(horizontalInput) > 0)
        {
            rigidbody.AddForce(new Vector3(0, 0, (rigidbody.velocity.magnitude * 120) * horizontalInput * Time.deltaTime), ForceMode.Acceleration);
        }
    }
}
