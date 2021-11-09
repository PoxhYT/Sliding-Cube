using Newtonsoft.Json;
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

    public FirebaseManager firebaseManager;

    public TMPro.TMP_Text CurrentSkin;

    private bool SetupButton = false;
    private bool BoughtSkin = false;

    public List<TMPro.TMP_Text> buttonTextList = new List<TMPro.TMP_Text>();

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
        AddBuyFunctionToButton("PoxhYT");
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

    private IEnumerator EndBuyPhase()
    {
        yield return new WaitForSecondsRealtime(1);
        BoughtSkin = false;
    }

    public async void AddBuyFunctionToButton(string username)
    {
        if(!SetupButton)
        {
            var user = await firebaseManager.GetUser(username);
            List<SkinInfo> skinInfos = user.skins;

            foreach (Button button in FindObjectsOfType<Button>())
            {
                if (button.name.Contains("CUBE"))
                {
                    button.onClick.AddListener(() =>
                    {
                        if (!BoughtSkin)
                        {
                            for (int i = 0; i < skinInfos.Count; i++)
                            {
                                SkinInfo skinInfo = skinInfos[i];
                                if(skinInfo.skinname == CurrentSkin.text)
                                {
                                    Debug.Log("YEESSSSS");
                                    if (!skinInfo.bought)
                                    {
                                        skinInfo.bought = true;

                                        Debug.Log("--------------------");
                                        string json = JsonConvert.SerializeObject(skinInfos);
                                        Debug.Log(json);
                                        Debug.Log("--------------------");

                                        firebaseManager.UpdateUser(username, "skins", json);
                                        ChangeButtonState();
                                        Debug.Log("Bought skin: " + CurrentSkin.text);

                                        BoughtSkin = true;
                                        StartCoroutine(EndBuyPhase());
                                    }
                                }
                            }
                        }
                    });
                }
            }
            Debug.Log("Finished");
            SetupButton = true;
        }
    }

    private async void ChangeButtonState()
    {
        foreach (TMPro.TMP_Text ButtonText in FindObjectsOfType<TMPro.TMP_Text>())
        {
            if (ButtonText.name.Contains("CUBE"))
            {
                User user = await firebaseManager.GetUser("PoxhYT");
                foreach (SkinInfo skinFinal in user.skins)
                {
                    if (skinFinal.skinname == ButtonText.name)
                    {
                        if (skinFinal.bought)
                        {
                            ButtonText.text = "Select";
                        }
                        else
                        {
                            ButtonText.text = "Buy";
                        }
                    }
                }
            }
        }
    }

    public async void BuySkin()
    {
        foreach(GameObject skin in GameObject.FindGameObjectsWithTag("skins"))
        {
            if (skin.name == CurrentSkin.text)
            {
                skin.transform.localScale = new Vector3(1, 1, 1);
                foreach (TMPro.TMP_Text ButtonText in FindObjectsOfType<TMPro.TMP_Text>())
                {
                    if (ButtonText.name.Contains("CUBE"))
                    {

                        User user = await firebaseManager.GetUser("PoxhYT");
                        foreach (SkinInfo skinFinal in user.skins)
                        {
                            if (skinFinal.skinname == ButtonText.name)
                            {

                                skin.transform.localScale = new Vector3(1, 1, 1);

                                if (skinFinal.bought)
                                {
                                    ButtonText.text = "Select";
                                }
                                else
                                {
                                    ButtonText.text = "Buy";
                                }
                            }
                        }
                    }
                }
            } else
            {
                skin.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }
}
