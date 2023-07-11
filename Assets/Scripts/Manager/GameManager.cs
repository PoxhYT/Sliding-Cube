using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private AudioManager _audioManager;
    
    private void Start()
    {
        _playerRigidbody = FindObjectOfType<Rigidbody>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(nextSceneIndex);
    }
    
    public void StartPreviousLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex -1;
        if (nextSceneIndex >= 0) SceneManager.LoadScene(nextSceneIndex);
    }
    
    public void FinishLevel()
    {
        _playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        FindObjectOfType<ScreenTransition>().StartTransition(false);
        _audioManager.FadeToMuffled(0, 300.0f, 2);
    }
    
    public void EndGame(GameObject player, GameObject LevelProgressUI)
    {
        Destroy(player);
        StartCoroutine(RestartLevelCoroutine());
        LevelProgressUI.SetActive(false);
    }

    IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
