using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject Transition;
    public GameObject MainMenu;
    public GameObject SelectionMenu;
    public GameObject UsernameSettings;
    public GameObject Settings;
    private GameObject CurrentGameObject;
    public GameObject LevelSelection;
    public GameObject ItemShop;

    public FirebaseManager firebaseManager;

    public TMPro.TMP_Text CurrentSkin;
    public TMPro.TMP_Text Username;

    private bool BoughtSkin = false;
    private bool isInMainMenu = true;
    private bool isLoadingGame = false;
    private bool IsInShop = false;
    private bool FoundButtons = false;

    public ModalWindowManager modalWindowManager;

    private void Update()
    {
        if (isInMainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(StartTransition(MainMenu, SelectionMenu));
                isInMainMenu = false;
            }
        }
        SelectLevel();

        if(IsInShop)
        {
            AddBuyFunctionToButton("PoxhYT");
        }
    }

    public IEnumerator StartTransition(GameObject LastMenu, GameObject TargetMenu)
    {
        Transition.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        LastMenu.SetActive(false);
        TargetMenu.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        Transition.SetActive(false);
        CurrentGameObject = TargetMenu;
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
                if(level.name.Contains("LEVEL"))
                {
                    LoadGame(level.gameObject);
                }
            });
        }
    }

    private IEnumerator EndBuyPhase()
    {
        yield return new WaitForSecondsRealtime(1);
        BoughtSkin = false;
    }

    private IEnumerator SetupBuyButtons()
    {
        AddBuyFunctionToButton("PoxhYT");
        yield return new WaitForSecondsRealtime(2);
        FoundButtons = true;
        Debug.Log("Finished: " + FoundButtons);
    }

    public async void AddBuyFunctionToButton(string username)
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
                            if (skinInfo.skinname == CurrentSkin.text)
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

                Debug.Log("Added listener on: " + button.name);
            }
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

    public void OpenSettings()
    {
        if (IsBasicUser())
        {
            StartCoroutine(StartTransition(SelectionMenu, UsernameSettings));
        }
        else
        {
            StartCoroutine(StartTransition(SelectionMenu, Settings));
        }
    }

    private string GetPath()
    {
        return Application.dataPath + "/StreamingAssets/user.json";
    }

    private User UserFromJSON()
    {
        string json = File.ReadAllText(GetPath());
        Debug.Log(json);
        return JsonUtility.FromJson<User>(json);
    }

    private bool IsBasicUser()
    {
        User user = UserFromJSON();
        return user.username.Contains("Player");
    }

    public void ChangeUsername()
    {
        if(IsBasicUser())
        {
            User user = UserFromJSON();
            user.username = Username.text;

            Debug.Log(user.username);

            string jsonUser = JsonUtility.ToJson(user);

            File.Delete(GetPath());

            File.WriteAllText(GetPath(), jsonUser);
            Debug.Log("Changed username to: " + Username.text);
        }
        modalWindowManager.CloseWindow();
        StartCoroutine(StartTransition(UsernameSettings, SelectionMenu));
    }

    public bool ChangedUsername()
    {
        string json = File.ReadAllText(GetPath());
        Debug.Log("Json: " + json);
        User user = JsonUtility.FromJson<User>(json);
        return user.username == "Player";
    }

    public void OpenMenu(string TargetObject)
    {
        foreach (GameObject gameObject in FindAllObjectsInScene())
        {
            if (gameObject.name == TargetObject)
            {
                StartCoroutine(StartTransition(CurrentGameObject, gameObject));
                CurrentGameObject = gameObject;
            }
        }
    }

    public void OpenItemShop()
    {
        StartCoroutine(StartTransition(SelectionMenu, ItemShop));
        IsInShop = true;
    }

    private List<GameObject> FindAllObjectsInScene()
    {
        Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        GameObject[] rootObjects = activeScene.GetRootGameObjects();
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        List<GameObject> objectsInScene = new List<GameObject>();

        for (int i = 0; i < rootObjects.Length; i++)
        {
            objectsInScene.Add(rootObjects[i]);
        }

        for (int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i].transform.root)
            {
                for (int i2 = 0; i2 < rootObjects.Length; i2++)
                {
                    if (allObjects[i].transform.root == rootObjects[i2].transform && allObjects[i] != rootObjects[i2])
                    {
                        objectsInScene.Add(allObjects[i]);
                        break;
                    }
                }
            }
        }
        return objectsInScene;
    }
}
