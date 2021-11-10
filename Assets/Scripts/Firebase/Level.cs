using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class Level : MonoBehaviour
{
    public string levelname;
    public bool finished;
    public int attempts;

    public Level(string levelname, bool finished, int attempts)
    {
        this.levelname = levelname;
        this.finished = finished;
        this.attempts = attempts;
    }
}
