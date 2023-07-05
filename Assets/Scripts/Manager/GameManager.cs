using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Rigidbody playerRigidbody;

    public void StartNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(nextSceneIndex);
    }
    
    public void FinishLevel()
    {
        StartCoroutine(FreezePlayerCoroutine());
    }
    
    public void EndGame(GameObject player, GameObject LevelProgressUI)
    {
        Destroy(player);
        StartCoroutine(RestartLevelCoroutine());
        LevelProgressUI.SetActive(false);
    }
    
    IEnumerator FreezePlayerCoroutine()
    {
        yield return new WaitForSeconds(2f);
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
    }
    
    IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
