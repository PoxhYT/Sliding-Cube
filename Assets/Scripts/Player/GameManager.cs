using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject LevelNotCompleted;

    [HideInInspector]
    public bool GameHasEnded = false;

    public void EndGame()
    {
        if(!GameHasEnded)
        {
            GameHasEnded = true;
            Player.gameObject.SetActive(false);
            LevelNotCompleted.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
