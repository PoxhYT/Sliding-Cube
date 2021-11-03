using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColission : MonoBehaviour
{
    public GameObject player;
    public bool gameEnd = false;
    public GameObject LevelNotCompleted;
    private void OnCollisionEnter(Collision collision)
    {
        if(ColissionWithObstacle(collision.collider.tag))
        {
            gameEnd = true;
            player.gameObject.SetActive(false);
            LevelNotCompleted.SetActive(true);
        }          
    }  

    private bool ColissionWithObstacle(string colissionName)
    {
        if(colissionName == "Obstacle") return true;
        return false;
    }
}
