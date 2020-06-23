using System;

public interface IAchievementsController
{
    bool CheckMoneyAchievement(int currentMoney);
    bool CheckGamePointsAchievement(int gamePoints);
    bool CheckFiveTimesOneDayAchievement(int dailyCount);
    void FirstGameAchievement();
    void AchievementIncremental(DateTime lastLoginDate);
}