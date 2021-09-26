using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;


public class PlayGames : MonoBehaviour
{
    public int playerScore;

    //LeaderBoard id from Play Console
    private string leaderboardID = "CgkItqSPyeYfEAIQAA";

    //Achievements id from play console 
    private string achvNoobie = "CgkItqSPyeYfEAIQAQ";
    private string achvStarter = "CgkItqSPyeYfEAIQAg";
    private string achvProStarter = "CgkItqSPyeYfEAIQAw";
    private string achvCity = "CgkItqSPyeYfEAIQBA";
    private string achvCountry = "CgkItqSPyeYfEAIQBQ";
    private string achvContinuous = "CgkItqSPyeYfEAIQBg";
    private string achvLoveGame = "CgkItqSPyeYfEAIQBw";
    private string achvRocking = "CgkItqSPyeYfEAIQCA";
    private string achvProGamer = "CgkItqSPyeYfEAIQCQ";

    public static PlayGamesPlatform platform;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex==0)
        {
            if (platform == null)
            {
                PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
                PlayGamesPlatform.InitializeInstance(config);
                PlayGamesPlatform.DebugLogEnabled = true;
                platform = PlayGamesPlatform.Activate();
            }

            Social.Active.localUser.Authenticate(success =>
            {
                if (success)
                {
                    Debug.Log("Logged in successfully");
                }
                else
                {
                    Debug.Log("Login Failed");
                }
            });
        }
        
       // UnlockAchievement();
    }

    //Access on death 
    public void AddScoreToLeaderboard(int ScoreFromGame)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(ScoreFromGame, leaderboardID, success => { });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowAchievementsUI();
        }
    }

    public void UnlockAchievement()
    {
        /*
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportProgress(achievementID, 100f, success => { });
        }
        */
    }
}
