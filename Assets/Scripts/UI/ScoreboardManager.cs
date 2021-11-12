using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    public Transform ScoreboardContainer;
    public Transform ScoreboardElement;
    private List<User> UserList = new List<User>();
    private List<Transform> scoreboardEntryTransfomList;

    public FirebaseManager firebaseManager;

    private void Awake()
    {
        ScoreboardElement.gameObject.SetActive(false);
    }

    private void Start()
    {
        AddUserDataToScoreboard();
    }

    private async void AddUserDataToScoreboard()
    {
        DataSnapshot snapshot = await firebaseManager.GetUsers();

        foreach (DataSnapshot childSnapshot in snapshot.Children)
        {
            var user = JsonUtility.FromJson<User>(childSnapshot.GetRawJsonValue());
            if (!UserList.Contains(user))
            {
                string json = JsonUtility.ToJson(user);
                Debug.Log(json);
                UserList.Add(user);
            }
        }

        SortScoreboardEntryList();

        scoreboardEntryTransfomList = new List<Transform>();
        int AllUsers = 0;

        Debug.Log(UserList.Count);

        for (int i = 0; i < UserList.Count; i++)
        {
            User user = UserList[i];

            CreateHighscoreEntryTransform(user.levels[0].attempts, user.username, ScoreboardContainer, scoreboardEntryTransfomList, false);
            AllUsers++;
        }

        for (int i = 0; i < 12 - AllUsers; i++)
        {
            CreateHighscoreEntryTransform(0, "NO DATA", ScoreboardContainer, scoreboardEntryTransfomList, true);
        }
    }

    private void SortScoreboardEntryList()
    {

        for (int i = 0; i < UserList.Count; i++)
        {
            for (int k = 0; k < UserList.Count; k++)
            {
                if(UserList[k].levels[0].attempts > UserList[i].levels[0].attempts)
                {
                    User user = UserList[i];
                    UserList[i] = UserList[k];
                    UserList[k] = user;
                }
            }
        }
    }

    private void CreateHighscoreEntryTransform(int attempts, string usernameFinal, Transform container, List<Transform> transforms, bool NoData)
    {

        Transform EntryTransform = Instantiate(ScoreboardElement, ScoreboardContainer);
        RectTransform EntryRectTransform = EntryTransform.GetComponent<RectTransform>();
        EntryRectTransform.anchoredPosition = new Vector2(0, -70 * transforms.Count);
        EntryRectTransform.gameObject.SetActive(true);

        int Rank = transforms.Count + 1;
        string RankString;
        switch (Rank)
        {
            default:
                RankString = Rank + "TH"; break;
            case 1: RankString = "1ST"; break;
            case 2: RankString = "2ND"; break;
            case 3: RankString = "3RD"; break;
        }

        EntryTransform.Find("placement").GetComponent<TMPro.TMP_Text>().text = RankString;

        int atempts = attempts;

        TMPro.TMP_Text atemptsText = EntryTransform.Find("atempts").GetComponent<TMPro.TMP_Text>();
        TMPro.TMP_Text username = EntryTransform.Find("username").GetComponent<TMPro.TMP_Text>();

        if (NoData)
        {
            atemptsText.text = "NO DATA";
            username.text = "NO DATA";
        } else
        {
            atemptsText.text = atempts.ToString();
            username.text = usernameFinal;
        }

        transforms.Add(EntryTransform);
    }

    private class ScoreboardEntry
    {
        public int atempts;
        public string name;
    }
}
