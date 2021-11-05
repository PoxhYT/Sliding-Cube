using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject Transition;
    public GameObject MainMenu;
    public GameObject SelectionMenu;

    private bool isInMainMenu = true;
    private bool isInSelectionMenu = false;

    private void Update()
    {
        if(isInMainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GoToSelectionMenu();
            }
        }

        if(isInSelectionMenu)
        {
            if(SceneManager.GetActiveScene().name == "MainScreen")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartGame();
                }
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LEVEL-01-FOREST");
    }

    public void GoToSelectionMenu()
    {
        StartCoroutine(StartTransition(SelectionMenu));    
    }

    public IEnumerator StartTransition(GameObject TargetMenu)
    {
        Transition.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        MainMenu.SetActive(false);
        TargetMenu.SetActive(true);
        isInSelectionMenu = true;
    }
}
