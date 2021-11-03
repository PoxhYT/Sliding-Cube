using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform player;
    public Vector3 offset = new Vector3(0, 1, -5);
    private PlayerColission playerColission = new PlayerColission();

    // Update is called once per frame
    void Update()
    {

        if (!playerColission.gameEnd) {
            transform.position = player.position + offset;
        }  
    }
}
