public class Achievement
{
    public string achievement_name;
    public string achievement_description;
    public bool isCompleted;

    public Achievement(string achievement_name, string achievement_description, bool isCompleted)
    {
        this.achievement_name = achievement_name;
        this.achievement_description = achievement_description;
        this.isCompleted = isCompleted;
    }
}
