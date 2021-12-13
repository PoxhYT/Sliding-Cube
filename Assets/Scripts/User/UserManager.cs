using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public UIManager manager;
    public GameObject information;

    private string GetPath()
    {
        return Application.dataPath + "/StreamingAssets/user.json";
    }

    public User initializeUser(string username)
    {
        User user = new User(username, 100, new List<SkinInfo>(), new List<Level>(), new List<Achievement>());
        initializeSkins(user.skins);
        initializeLevels(user.levels);
        initializeAchievements(user.achievements);
        return user;
    }

    private void initializeSkins(List<SkinInfo> skins)
    {
        skins.Add(new SkinInfo("Standart", 0, true));
        skins.Add(new SkinInfo("Fortnite", 1000, false));
        skins.Add(new SkinInfo("Galaxy", 2000, false));
        skins.Add(new SkinInfo("Plexus", 3000, false));
        skins.Add(new SkinInfo("Sakura", 4000, false));
    }

    private void initializeLevels(List<Level> levels)
    {
        levels.Add(new Level("LEVEL-01-FOREST", false, 0));
        levels.Add(new Level("LEVEL-02-HIGHWAY", false, 0));
    }

    private void initializeAchievements(List<Achievement> achievements)
    {
        achievements.Add(new Achievement("Welcome to Sliding Cube", "Play Sliding Cube for the first time", false));
        achievements.Add(new Achievement("DIE!", "Die for the first time", false));
    }

    public User UserJSON()
    {
        string json = File.ReadAllText(GetPath());
        Debug.Log(json);
        return JsonUtility.FromJson<User>(json);
    }

    public bool IsUserRegistered()
    {
        User user = UserJSON();
        return user.username.Contains("Player");
    }

    public void ChangeUsername()
    {
        if (!IsUserRegistered())
        {

            string username = manager.Username.text;

            if (username == "")
            {
                Debug.Log("NULL!");
                SendInformationToUser("Enter a valid username!");
                return;
            }

            User user = UserJSON();
            user.username = manager.Username.text;

            Debug.Log(user.username);

            string jsonUser = JsonUtility.ToJson(user);

            File.Delete(GetPath());

            File.WriteAllText(GetPath(), jsonUser);
            Debug.Log("Changed username to: " + manager.Username.text);
        }
        /*manager.modalWindowManager.CloseWindow();
        StartCoroutine(manager.StartTransition(manager.UsernameSettings, manager.SelectionMenu));*/
    }

    public bool ChangedUsername()
    {
        string json = File.ReadAllText(GetPath());
        Debug.Log("Json: " + json);
        User user = JsonUtility.FromJson<User>(json);
        return user.username == "Player" || user.username == "";
    }

    public void SendInformationToUser(string informationText)
    {
        information.SetActive(true);

        foreach (Component element in information.GetComponents(typeof(Component)))
        {
            Debug.Log("ELEMENT: " + element.tag);
        }

        /*TMP_Text content = information.GetComponent<TMP_Text>();
        content.text = informationText;*/
    }
}
