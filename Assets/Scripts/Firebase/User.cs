using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public string username;
    public List<string> finishedLevel;

    public User()
    {
    }

    public User(string username, List<string> finishedLevel)
    {
        this.username = username;
        this.finishedLevel = finishedLevel;
    }
}
