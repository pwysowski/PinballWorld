using System;
public interface IGPSController
{
    void InitializeGPS();
    void CompleteAchievement(string achievementKey);
    void IncrementAchievement(string achievementKey, int value);
    void ShowAchievements();
    void ShowLeaderboards();
    void AddScoreToLeaderboard(long score);

    Action LoginSuccess { get; set; }
    Action LoginFailure { get; set; }
    Action AchievementSuccess { get; set; }
    Action AchievementFailure { get; set; }
    Action IncrementAchievementSuccess {get; set;}
    Action IncrementAchievementFailure {get; set;}
}