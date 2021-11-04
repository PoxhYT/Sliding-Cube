using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform player;
    private Vector3 offset = new Vector3(0, 1, -5);

    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        
        transform.position = player.position + offset;
    }
}
