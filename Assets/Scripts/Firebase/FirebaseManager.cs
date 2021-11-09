using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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
            User user = new User(username.text, new List<string>(), new List<SkinInfo>());
            user.finishedLevel.Add("LEVEL-01-FOREST");
            user.skins.Add(new SkinInfo("RED-CUBE", 0, true));
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

    public async void test()
    {
        await GetUser(username.text);
    }

    public async Task<User> GetUser(string username)
    {
        var task = await reference.Child("users").Child(username).GetValueAsync();

        DataSnapshot snapshot = task;

        var UserExist = await IsUserInDatabase(username);

        if(UserExist)
        {
            var user = JsonUtility.FromJson<User>(snapshot.GetRawJsonValue());
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
        return snapshot.Child("username").Value != null;
    }

    public async void UpdateUser(string username, string path, string json)
    {
        await reference.Child("users").Child(username).Child("skins").SetRawJsonValueAsync(json);
        Debug.Log("Updated user!");
    }
}
