﻿using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class FirebaseManager : MonoBehaviour
{
    [SerializeField]
    private List<Level> levels = new List<Level>(); 

    [SerializeField] TMPro.TMP_InputField username;

    [HideInInspector]
    public DatabaseReference reference;

    public UserManager userManager;

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

    public async void AddUserToDatabase(string username)
    {
        var UserInDatabase = await IsUserInDatabase(username);

        if (!UserInDatabase)
        {
            User user = userManager.initializeUser(username);
            string json = JsonUtility.ToJson(user);

            try
            {
                await reference.Child("users").Child(user.username).SetRawJsonValueAsync(json);
                Debug.Log("Added " + username + " to the database");
            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e);
            }
        }
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
        Debug.Log(snapshot);
        return snapshot.Child("username").Value != null;
    }

    public async void UpdateUser(string username, string path, string json)
    {
        await reference.Child("users").Child(username).Child(path).SetRawJsonValueAsync(json);
        Debug.Log("Updated user!");
    }

    public async Task<DataSnapshot> GetUsers()
    {
        var task = await reference.Child("users").GetValueAsync();
        return task;
    }

    public async void UpdateUserLevelAttempt(string username, string currentLevel)
    {
        var user = await GetUser(username);
        List<Level> levelList = user.levels;

        for (int i = 0; i < levelList.Count; i++)
        {
            Level level = levelList[i];
            if(level.levelname.Contains(currentLevel))
            {
                levels.Add(level);

                Debug.Log("OLD: " + level.attempts);
                level.attempts++;
                Debug.Log("NEW: " + level.attempts);

                string json = JsonConvert.SerializeObject(levelList);
                Debug.Log("--------------------");
                Debug.Log(json);
                Debug.Log("--------------------");

                UpdateUser(username, "levels", json);
            }
        }
    }
}
