using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject Transition;
    public GameObject MainMenu;
    public GameObject SelectionMenu;

    private bool isInMainMenu = true;
    private bool isInSelectionMenu = false;
    private bool isLoadingGame = false;

    private void Update()
    {
        if (isInMainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GoToSelectionMenu();
            }
        }

        if (isInSelectionMenu)
        {
            if (SceneManager.GetActiveScene().name == "MainScreen")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartGame();
                }
            }
        }
        SelectLevel();
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
        yield return new WaitForSecondsRealtime(1);
        Transition.SetActive(false);
    }

    private void LoadGame(GameObject level)
    {
        if (!isLoadingGame)
        {
            SceneManager.LoadScene(level.name);
            isLoadingGame = true;
        }
    }

    public void SelectLevel()
    {
        Button[] levels = Button.FindObjectsOfType<Button>();
        foreach (Button level in levels)
        {
            level.onClick.AddListener(() =>
            {
                LoadGame(level.gameObject);
            });
        }
    }
}
