using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void restartGame()
    {
        SceneManager.LoadScene("Level01");
    }
}
