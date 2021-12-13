using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AchievementManager
{
    public FirebaseManager firebaseManager;
    public UserManager userManager;

    public async Task<bool> userPlayedFirstTime()
    {
        string username = userManager.UserJSON().username;
        User user = await firebaseManager.GetUser(username);
        return user.levels[0].attempts == 0;
    }

    public async Task<bool> userDiedFirstTime()
    {
        string username = userManager.UserJSON().username;
        User user = await firebaseManager.GetUser(username);
        return user.levels[0].attempts == 1;
    }

    public void unlockAchievement(string achievement_name)
    {
        
    }
}
