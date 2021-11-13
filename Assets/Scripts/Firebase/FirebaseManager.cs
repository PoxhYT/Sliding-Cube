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
    [HideInInspector]
    public DatabaseReference reference;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private async void test(string username, int atempts, int price)
    {
        User user = new User(username, 1000, new List<SkinInfo>(), new List<Level>());
        user.levels.Add(new Level("LEVEL-01-FOREST", true, atempts));
        user.skins.Add(new SkinInfo("RED-CUBE", price, true));
        string json = JsonUtility.ToJson(user);

        await reference.Child("users").Child(user.username).SetRawJsonValueAsync(json);
        Debug.Log("Added " + username + " to the database");
    }

    public async void AddUserToDatabase(string username)
    {
        var UserInDatabase = await IsUserInDatabase(username);

        if (!UserInDatabase)
        {
            User user = new User(username, 1000, new List<SkinInfo>(), new List<Level>());
            user.levels.Add(new Level("LEVEL-01-FOREST", true, 0));
            user.skins.Add(new SkinInfo("RED-CUBE", 0, true));
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
        return snapshot.Child("username").Value != null;
    }

    public async void UpdateUser(string username, string path, string json)
    {
        await reference.Child("users").Child(username).Child("skins").SetRawJsonValueAsync(json);
        Debug.Log("Updated user!");
    }

    public async Task<DataSnapshot> GetUsers()
    {
        Debug.Log(reference);
        var task = await reference.Child("users").GetValueAsync();
        return task;
    }
}
