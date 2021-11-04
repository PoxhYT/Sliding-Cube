using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject LevelCompleted;

    public void restartGame()
    {
        SceneManager.LoadScene("LEVEL-01-FOREST");
    }
}
