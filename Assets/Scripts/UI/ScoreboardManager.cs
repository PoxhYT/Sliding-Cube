using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    public Transform ScoreboardContainer;
    public Transform ScoreboardElement;
    private List<ScoreboardEntry> scoreboardEntries;
    private List<Transform> scoreboardEntryTransfomList;

    public FirebaseManager firebaseManager;

    private void Awake()
    {
        ScoreboardElement.gameObject.SetActive(false);

        scoreboardEntries = new List<ScoreboardEntry>()
        {
            new ScoreboardEntry{ atempts = 12, name = "AAA" },
            new ScoreboardEntry{ atempts = 24, name = "BBB" },
            new ScoreboardEntry{ atempts = 8, name = "CCC" },
            new ScoreboardEntry{ atempts = 21, name = "DDD" },
            new ScoreboardEntry{ atempts = 2, name = "EEE" },
            new ScoreboardEntry{ atempts = 5, name = "FFF" },
            new ScoreboardEntry{ atempts = 30, name = "GGG" },
            new ScoreboardEntry{ atempts = 9, name = "HHH" },
            new ScoreboardEntry{ atempts = 1, name = "III" },
            new ScoreboardEntry{ atempts = 3, name = "JJJ" },
        };

        firebaseManager.reference = FirebaseDatabase.DefaultInstance.RootReference;

        firebaseManager.AddUserToDatabase("PoxhYT");
        firebaseManager.AddUserToDatabase("PoxhYT2");
        firebaseManager.AddUserToDatabase("PoxhYT3");
        firebaseManager.AddUserToDatabase("PoxhYT4");
        firebaseManager.AddUserToDatabase("PoxhYT5");
        firebaseManager.AddUserToDatabase("PoxhYT6");
        firebaseManager.AddUserToDatabase("PoxhYT7");
        firebaseManager.AddUserToDatabase("PoxhYT8");
        firebaseManager.AddUserToDatabase("PoxhYT9");
        firebaseManager.AddUserToDatabase("PoxhYT10");
        firebaseManager.AddUserToDatabase("PoxhYT11");
        firebaseManager.AddUserToDatabase("PoxhYT12");

        var task = firebaseManager.reference.Child("users").GetValueAsync();
        DataSnapshot snapshot = task.Result;

        //Loop through every users UID
        foreach (DataSnapshot childSnapshot in snapshot.Children)
        {
            Debug.Log(childSnapshot);

            //Instantiate new scoreboard elements
        }

        SortScoreboardEntryList();

        scoreboardEntryTransfomList = new List<Transform>();
        foreach (ScoreboardEntry scoreboardEntry in scoreboardEntries)
        {
            CreateHighscoreEntryTransform(scoreboardEntry, ScoreboardContainer, scoreboardEntryTransfomList);
        }
    }

    private void SortScoreboardEntryList()
    {
        for (int i = 0; i < scoreboardEntries.Count; i++)
        {
            for (int k = 0; k < scoreboardEntries.Count; k++)
            {
                if(scoreboardEntries[k].atempts > scoreboardEntries[i].atempts)
                {
                    ScoreboardEntry scoreboardEntry = scoreboardEntries[i];
                    scoreboardEntries[i] = scoreboardEntries[k];
                    scoreboardEntries[k] = scoreboardEntry;
                }
            }
        }
    }

    private void CreateHighscoreEntryTransform(ScoreboardEntry scoreboardEntry, Transform container, List<Transform> transforms)
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

        int atempts = scoreboardEntry.atempts;
        EntryTransform.Find("atempts").GetComponent<TMPro.TMP_Text>().text = atempts.ToString();
        EntryTransform.Find("username").GetComponent<TMPro.TMP_Text>().text = scoreboardEntry.name;

        transforms.Add(EntryTransform);
    }

    private class ScoreboardEntry
    {
        public int atempts;
        public string name;
    }
}
