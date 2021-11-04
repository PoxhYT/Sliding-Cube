using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColission : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(ColissionWithObstacle(collision.collider.tag))
        {
            FindObjectOfType<GameManager>().EndGame();
        }          
    }  

    private bool ColissionWithObstacle(string colissionName)
    {
        if(colissionName == "Obstacle") return true;
        return false;
    }
}
