using System;
public class AchievementsController : IAchievementsController
{
    private readonly IGPSController _gpsController;

    private static int SINGLE_PLAY_POINTS_AMMOUNT = 10000;
    private static int MONEY_AMMOUNT = 1000;
    private static int DAILY_PLAYS = 5;
    public AchievementsController(IGPSController gpsController)
    {
        _gpsController = gpsController;
    }

    public bool CheckMoneyAchievement(int currentMoney)
    {
        return currentMoney >= MONEY_AMMOUNT;
    }
    public bool CheckGamePointsAchievement(int gamePoints)
    {
        return gamePoints >= SINGLE_PLAY_POINTS_AMMOUNT;
    }
    public bool CheckFiveTimesOneDayAchievement(int dailyCount)
    {
        return dailyCount == DAILY_PLAYS;
    }

    public void AchievementIncremental(DateTime lastLoginDate)
    {
        if (lastLoginDate.Date < DateTime.Now.Date)
        {
            _gpsController.IncrementAchievement(GPGSIds.achievement_incremental_achievement_1, 1);
        }
    }

    public void FirstGameAchievement()
    {
        _gpsController.CompleteAchievement(GPGSIds.achievement_achievement_5);
    }

}