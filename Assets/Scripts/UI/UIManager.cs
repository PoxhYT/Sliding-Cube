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

    private void Update()
    {
        if(isInMainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GoToSelectionMenu();
            }
        }
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
    }
}
