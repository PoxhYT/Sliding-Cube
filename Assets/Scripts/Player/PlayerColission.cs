using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColission : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(ColissionWithObstacle(collision.collider.name))
        {
            Debug.Log("You lost!");
        }          
    }

    private bool ColissionWithObstacle(string colissionName)
    {
        if(colissionName == "Obstacle") return true;
        return false;
    }
}
