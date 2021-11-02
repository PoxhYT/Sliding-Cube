using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forceSpeed = 500;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Programm starts!");
        Debug.Log("Position: " + rb.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AddForceToRigidbody(0, 0, forceSpeed * Time.deltaTime);
        SetupControlls();
    }

    private void AddForceToRigidbody(float valueA, float valueB, float valueC)
    {
        Vector3 force = new Vector3(valueA, valueB, valueC);
        rb.AddForce(force);
    }

    private void SetupControlls()
    {
        if (Input.GetKey("d"))
        {
            rb.AddForce(50 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        else if (Input.GetKey("a"))
        {
            rb.AddForce(-50 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
    }

    private float getPlayersCurrentSpeed()
    {
        Vector3 playersVelocity = rb.velocity;      
        return playersVelocity.magnitude;
    }
}
