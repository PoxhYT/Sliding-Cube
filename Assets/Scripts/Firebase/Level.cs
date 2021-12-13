using System;

[Serializable]
public class Level
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
