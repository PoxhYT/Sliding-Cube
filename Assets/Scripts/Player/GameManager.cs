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

    public List<GameObject> gameObjects = new List<GameObject>();

    public void EndGame()
    {
        if(!GameHasEnded)
        {
            GameHasEnded = true;
            Player.gameObject.SetActive(false);
            LevelNotCompleted.SetActive(true);
        }
    }

    public IEnumerator FinishGame()
    {
        if (!GameHasEnded)
        {
            GameHasEnded = true;
            LevelCompleted.SetActive(true);
            yield return new WaitForSecondsRealtime(1);
            Player.gameObject.SetActive(false);
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

    public float GetDistance(Rigidbody player, Transform endPosition)
    {
        return Vector3.Distance(player.position, endPosition.position);
    }

    public float GetCurrentProgress(Rigidbody player, Transform endPosition, float fullDistance)
    {
        if (player.transform.position.z <= endPosition.position.z)
        {
            float newDistance = GetDistance(player, endPosition);
            float progress = Mathf.InverseLerp(fullDistance, 0f, newDistance);
            return Mathf.Round(progress * 100);
        }
        return 100.0f;
    }

    public void UnloadChunck(Rigidbody player)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            GameObject gameObject = gameObjects[i];

            float positionGameObject = Mathf.Round(gameObject.transform.position.z);
            float positionPlayer = Mathf.Round(player.transform.position.z);

            if (positionGameObject + 5 < positionPlayer)
            {
                Destroy(gameObject);
                gameObjects.Remove(gameObject);
            }
        }
    }
}
