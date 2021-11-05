using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColission : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (ColissionWithObstacle(collision.collider.tag))
        {
            Debug.Log("DED");
            FindObjectOfType<GameManager>().EndGame();
        }          
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "END")
        {
            StartCoroutine(FindObjectOfType<GameManager>().FinishGame());
        }
    }

    private bool ColissionWithObstacle(string colissionName)
    {
        if(colissionName == "Obstacle") return true;
        return false;
    }
}
