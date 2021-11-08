using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string username;
    public List<string> finishedLevel;
    public List<SkinInfo> skins;

    public User(string username, List<string> finishedLevel, List<SkinInfo> skins)
    {
        this.username = username;
        this.finishedLevel = finishedLevel;
        this.skins = skins;
    }
}
