using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColission : MonoBehaviour
{
    public FirebaseManager firebaseManager;
    public AchievementManager achievementManager;

    private string GetPath()
    {
        return Application.dataPath + "/StreamingAssets/user.json";
    }

    private User UserFromJSON()
    {
        string json = File.ReadAllText(GetPath());
        return JsonUtility.FromJson<User>(json);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);


        if (ColissionWithObstacle(collision.collider.tag))
        {
            firebaseManager.UpdateUserLevelAttempt("PoxhYT", SceneManager.GetActiveScene().name);
            FindObjectOfType<GameManager>().EndGame();
        }          
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if(other.name == "END")
        {
            TimerController.instance.StopTimer();
            StartCoroutine(FindObjectOfType<GameManager>().FinishGame());
        }
    }

    private bool ColissionWithObstacle(string colissionName)
    {
        if(colissionName == "Obstacle") return true;
        return false;
    }
}
