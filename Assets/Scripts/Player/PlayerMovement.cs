using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Programm starts!");
        Debug.Log("Position: " + rb.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AddForceToRigidbody(0, 0, 2000);    
    }

    private void AddForceToRigidbody(int valueA, int valueB, int valueC)
    {
        Vector3 force = new Vector3(valueA, valueB, valueC * Time.deltaTime);
        rb.AddForce(force);
    }
}
