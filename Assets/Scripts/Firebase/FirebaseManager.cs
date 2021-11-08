using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{


    [SerializeField] TMPro.TMP_InputField username;
    DatabaseReference reference;

    private void Start()
    {
        Debug.Log("HELLO!");

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                reference = FirebaseDatabase.DefaultInstance.RootReference;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private bool IsUserInDatabase(string username)
    {
        return GetUser(username) == null;
    }

    public async void AddUserToDatabase()
    {
        if (!IsUserInDatabase(username.text))
        {
            User user = new User(username.text, new List<string>());
            user.finishedLevel.Add("LEVEL-01-FOREST");
            string json = JsonUtility.ToJson(user);

            try
            {
                await reference.Child("users").Child(user.username).SetRawJsonValueAsync(json);
                Debug.Log("Added " + username.text + " to the database");
            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e);
            }
        } else
        {
            Debug.Log("NOOOOOOOOOOO");
        }
    }

    private async Task<User> GetUser(string username)
    {
        var task = await reference.Child("users").Child(username).GetValueAsync();

        User user = null;

        DataSnapshot snapshot = task;
        if (snapshot.Child("username").Value != null)
        {
            user = new User(username, new List<string>());

            foreach (var level in snapshot.Child("finishedLevel").Children)
            {
                user.finishedLevel.Add(level.Value.ToString());
                Debug.Log("Level: " + level.Value.ToString());
                Debug.Log("Count: " + user.finishedLevel.Count);
            }
        }
        return user;
    } 
}
