using Assets.Scripts;
using Assets.Scripts.Saves;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : IGameController
{

    public GameState CurrentState { get; set; }
    public int Money { get; set; }
    public Action<GameState> OnGameStateChange { get; set; }
    public bool FirstGamePlayed { get; set; }

    private IGPSController _gpsController;
    private IAchievementsController _achievementsController;
    private ISaveController _saveController;

    public GameController(IGPSController gpsController, IAchievementsController achievementsController, ISaveController saveController)
    {
        _gpsController = gpsController;
        _achievementsController = achievementsController;
        _saveController = saveController;

        Init();
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChange?.Invoke(gameState);
    }

    public void Init()
    {
        ChangeGameState(GameState.MENU);
        _gpsController.LoginSuccess += ProceedToLoading;
    }

    private void ProceedToLoading()
    {
        _gpsController.LoginSuccess -= ProceedToLoading;
        LoadGame();
    }

    private void LoadGame()
    {
        Money = _saveController.LoadMoney();

        int dailyCount = _saveController.LoadDailyCount();
        DateTime lastLoginDate = _saveController.LoadLastLogin();

        _achievementsController.AchievementIncremental(lastLoginDate);

        if (lastLoginDate.Date == DateTime.Now.Date)
        {
            dailyCount += 1;
            _saveController.SaveDailyCount(dailyCount);

            bool dailyAchievementCompleted = _achievementsController.CheckFiveTimesOneDayAchievement(dailyCount);
            if (dailyAchievementCompleted)
            {
                _gpsController.CompleteAchievement(GPGSIds.achievement_achievement_4);
            }
        }
        _saveController.SaveLastLogin(DateTime.Now);
        FirstGamePlayed = _saveController.LoadIsFirstGame();
    }

    public void ShowAchievementsUI()
    {
        _gpsController.ShowAchievements();
    }

    public void ShowLeaderboardsUI()
    {
        _gpsController.ShowLeaderboards();
    }

    public void SaveGamepoints(int points)
    {
        long score = Convert.ToInt64(points);
        _gpsController.AddScoreToLeaderboard(score);

        bool scoreAchievementCompleted = _achievementsController.CheckGamePointsAchievement(points);
        if (scoreAchievementCompleted)
        {
            _gpsController.CompleteAchievement(GPGSIds.achievement_achievement_1);
        }

        bool leaderboardsAchievementCompleted = _achievementsController.CheckLeaderboardsAchievement();

        if (leaderboardsAchievementCompleted)
        {
            _gpsController.CompleteAchievement(GPGSIds.achievement_achievement_2);
        }
    }

    public void CompletedFirstGame()
    {
        _saveController.SaveIsFirstGame();
        _achievementsController.FirstGameAchievement();
    }

    public void AddMoney(int money)
    {
        Money += money;
        _saveController.SaveMoney(Money);
        bool moneyAchievementCompleted = _achievementsController.CheckMoneyAchievement(Money);

        if (moneyAchievementCompleted)
        {
            _gpsController.CompleteAchievement(GPGSIds.achievement_achievement_3);
        }
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
