using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using System;
public class GPSController : MonoBehaviour, IGPSController
{
    public Action LoginSuccess { get; set; }
    public Action LoginFailure { get; set; }
    public Action AchievementSuccess { get; set; }
    public Action AchievementFailure { get; set; }
    public Action IncrementAchievementSuccess { get; set; }
    public Action IncrementAchievementFailure { get; set; }
    private void Awake()
    {
        InitializeGPS();
    }
    public void InitializeGPS()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .RequestEmail()
        .Build();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
            if (result.Equals(SignInStatus.Success))
            {
                LoginSuccess?.Invoke();
            }
            else
            {
                LoginFailure?.Invoke();
            }

        });
    }

    public void CompleteAchievement(string achievementKey)
    {
        Social.ReportProgress(achievementKey, 100.0f, (bool success) =>
        {
            if (success)
            {
                Social.ShowAchievementsUI();
            }
        });
    }

    public void IncrementAchievement(string achievementKey, int value)
    {
        bool status = false;
        PlayGamesPlatform.Instance.IncrementAchievement(
            achievementKey, value, (bool success) =>
            {
                status = success;
            });
    }

    public void AddScoreToLeaderboard(long score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_leaderboard_1_student_2, (bool success) =>
        {
            Debug.Log("Score added to leaderboard");
        });
    }

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboards()
    {
        Social.ShowLeaderboardUI();
    }
}