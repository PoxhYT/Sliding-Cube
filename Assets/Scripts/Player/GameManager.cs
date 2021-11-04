using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject LevelNotCompleted;
    public GameObject LevelCompleted;

    [HideInInspector]
    public bool GameHasEnded = false;

    [HideInInspector]
    public float start = 3f;
    [HideInInspector]
    public float currentTime = 0f;

    public void EndGame()
    {
        if(!GameHasEnded)
        {
            GameHasEnded = true;
            Player.gameObject.SetActive(false);
            LevelNotCompleted.SetActive(true);
        }
    }

    public void ChangeGravityState(Rigidbody rigidbody, Text score)
    {
        if (!isReadyToStart())
        {
            SetupCountdown(rigidbody, score);
        }
        else
        {
            score.enabled = false;
            rigidbody.useGravity = true;
        }
    }

    private void SetupCountdown(Rigidbody rigidbody, Text score)
    {
        rigidbody.useGravity = false;
        score.enabled = true;
        currentTime -= 1 * Time.deltaTime;
        score.text = currentTime.ToString("0");
    }

    public void SetCountdown()
    {
        currentTime = start;
    }

    public bool isReadyToStart()
    {
        return currentTime < 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
