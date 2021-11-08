using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string levelname;
    public bool finishedLevel;

    public Level()
    {

    }

    public Level(string level, bool finished)
    {
        levelname = level;
        finishedLevel = finished;
    }
}
