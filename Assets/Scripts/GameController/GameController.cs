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

    private bool moneyAchievementInactive;
    private bool scoreAchievementInactive;


    public GameController(IGPSController gpsController, IAchievementsController achievementsController, ISaveController saveController)
    {
        _gpsController = gpsController;
        _achievementsController = achievementsController;
        _saveController = saveController;

        Debug.Log("Game controller");
        Init();

    }

    public void ChangeGameState(GameState gameState)
    {
        CurrentState = gameState;
        OnGameStateChange?.Invoke(gameState);
    }

    public void Init()
    {
        _gpsController.LoginSuccess += ProceedToLoading;

        #if UNITY_EDITOR
            ProceedToLoading();
        #endif
    }

    private void ProceedToLoading()
    {
        _gpsController.LoginSuccess -= ProceedToLoading;
        LoadGame();
    }

    private void LoadGame()
    {
        Money = _saveController.LoadMoney();
        moneyAchievementInactive = _saveController.LoadMoneyAchievementDone();
        scoreAchievementInactive = _saveController.LoadScoreAchievementDone();

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

        ChangeGameState(GameState.MENU);
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
        if (scoreAchievementCompleted && !scoreAchievementInactive)
        {
            scoreAchievementInactive = true;
            _gpsController.CompleteAchievement(GPGSIds.achievement_achievement_1);
            _saveController.SaveScoreAchievementDone();
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

        if (moneyAchievementCompleted && !moneyAchievementInactive)
        {
            moneyAchievementInactive = true;
            _gpsController.CompleteAchievement(GPGSIds.achievement_achievement_3);
            _saveController.SaveMoneyAchievementDone();
        }
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
