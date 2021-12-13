using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string username;
    public int cubix;

    public List<SkinInfo> skins;
    public List<Level> levels;
    public List<Achievement> achievements;

    public User(string username, int cubix, List<SkinInfo> skins, List<Level> levels, List<Achievement> achievements)
    {
        this.username = username;
        this.cubix = cubix;
        this.levels = levels;
        this.skins = skins;
        this.achievements = achievements;
    }
}
