using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColission : MonoBehaviour
{
    public GameObject player;
    public bool gameEnd = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(ColissionWithObstacle(collision.collider.tag))
        {
            gameEnd = true;
            player.gameObject.SetActive(false);
        }          
    }

    private bool ColissionWithObstacle(string colissionName)
    {
        if(colissionName == "Obstacle") return true;
        return false;
    }
}
