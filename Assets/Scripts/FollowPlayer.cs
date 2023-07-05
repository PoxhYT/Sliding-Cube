using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public GameObject playerObject;
    public Vector3 offset;

    private void Update()
    {
        if (playerObject != null)
        {
            transform.position = player.position + offset;
        }
    }
}
