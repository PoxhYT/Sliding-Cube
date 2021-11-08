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

    public async void AddUserToDatabase()
    {
        var UserInDatabase = await IsUserInDatabase(username.text);

        if (!UserInDatabase)
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
        }
    }

    private async Task<User> GetUser(string username)
    {
        var task = await reference.Child("users").Child(username).GetValueAsync();


        DataSnapshot snapshot = task;

        var UserExist = await IsUserInDatabase(username);

        if(UserExist)
        {
            User user = new User(username, new List<string>());

            foreach (var level in snapshot.Child("finishedLevel").Children)
            {
                user.finishedLevel.Add(level.Value.ToString());
            }
            Debug.Log("User exist!");
            return user;
        } else
        {
            Debug.Log("User is null!");
            return null;
        }
    } 

    private async Task<bool> IsUserInDatabase(string username)
    {
        var task = await reference.Child("users").Child(username).GetValueAsync();

        DataSnapshot snapshot = task;

        Debug.Log("Snapshot: " + snapshot.Child("username"));

        return snapshot.Child("username").Value != null;
    }
}
