using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    public List<Transform> ScoreboardContainers;
    [HideInInspector]
    public Transform CurrentContainer;
    public Transform ScoreboardElement;
    private List<User> UserList = new List<User>();
    private List<Transform> scoreboardEntryTransfomList;

    public GameObject ProgressBar;
    public FirebaseManager firebaseManager;

    private bool ReceivedData = false;
    private bool ValueChanged = false;
    private bool AddedDataToScoreboard = false;

    private void Awake()
    {
        ScoreboardElement.gameObject.SetActive(false);
    }

    private void Start()
    {
        /*StartCoroutine(WaitForScoreboardUpdate());*/
        CurrentContainer = ScoreboardContainers[0];
        Debug.Log("FinalContainer: " + CurrentContainer);
        InvokeRepeating("UpdateScoreboard", 2.0f, 3.0f);
    }

    public void UpdateScoreboard()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").ChildChanged += HandleValueChanged;
    }

    private IEnumerator SetValueChangedState()
    {
        yield return new WaitForSecondsRealtime(3);
        ValueChanged = false;
    }

    void HandleValueChanged(object sender, ChildChangedEventArgs args)
    {
        if (!ValueChanged)
        {
            ValueChanged = true;

            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }
            Debug.Log(args.Snapshot);

            StartCoroutine(WaitForScoreboardUpdate());
            StartCoroutine(SetValueChangedState());
        }
    }

    public IEnumerator WaitForScoreboardUpdate()
    {
        ProgressBar.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        ProgressBar.SetActive(false);
        for (int i = 0; i < ScoreboardContainers.Count; i++)
        {
            if (ScoreboardContainers[i].name.Contains(CurrentContainer.name))
            {
                Transform Container = ScoreboardContainers[i];
                if(!AddedDataToScoreboard)
                {
                    AddUserDataToScoreboard(Container, CurrentContainer.name);
                    AddedDataToScoreboard = true;
                }
            }
        }
    }

    private void ClearScoreboardData(Transform Scoreboard)
    {
        foreach (Transform ScoreboardItem in Scoreboard)
        {
            Debug.Log("Removed: " + ScoreboardItem.name);
            Destroy(ScoreboardItem.gameObject);
        }

        UserList.Clear();
        ClearScoreboardData(Scoreboard);
    }

    private async void AddUserDataToScoreboard(Transform Scoreboard, string TargetLevel)
    {
        if(!ReceivedData)
        {
            DataSnapshot snapshot = await firebaseManager.GetUsers();

            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                var user = JsonUtility.FromJson<User>(childSnapshot.GetRawJsonValue());
                string json = JsonUtility.ToJson(user);

                UserList.Add(user);
                Debug.Log(json);
            }

            ReceivedData = true;
        }

        for (int i = 0; i < ScoreboardContainers.Count; i++)
        {
            if(ScoreboardContainers[i].name.Contains(TargetLevel))
            {
                SortScoreboardEntryList(i);
            }
        }

        scoreboardEntryTransfomList = new List<Transform>();
        int AllUsers = 0;

        Debug.Log(UserList.Count);

        for (int i = 0; i < UserList.Count; i++)
        {
            User user = UserList[i];

            for (int k = 0; k < user.levels.Count; k++)
            {
                if(user.levels[k].levelname.Contains(TargetLevel) && user.levels[k].finished && user.levels[k].attempts > 0)
                {
                    AllUsers++;

                    if(AllUsers < 13)
                    {
                        Level level = user.levels[k];
                        CreateHighscoreEntryTransform(level.attempts, user.username, scoreboardEntryTransfomList, false, Scoreboard);
                    }
                }
            }
        }

        for (int i = 0; i < 12 - AllUsers; i++)
        {
            CreateHighscoreEntryTransform(0, "NO DATA", scoreboardEntryTransfomList, true, Scoreboard);
        }
    }

    private void SortScoreboardEntryList(int index)
    {

        for (int i = 0; i < UserList.Count; i++)
        {
            for (int k = 0; k < UserList.Count; k++)
            {
                if(UserList[k].levels[index].attempts > UserList[i].levels[index].attempts)
                {
                    User user = UserList[i];
                    UserList[i] = UserList[k];
                    UserList[k] = user;
                }
            }
        }
    }

    private void CreateHighscoreEntryTransform(int attempts, string usernameFinal, List<Transform> transforms, bool NoData, Transform Scoreboard)
    {

        Transform EntryTransform = Instantiate(ScoreboardElement, Scoreboard);
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
