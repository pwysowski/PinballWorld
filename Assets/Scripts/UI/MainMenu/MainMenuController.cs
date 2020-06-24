using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using Assets.Scripts;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuContainer;

    [SerializeField]
    private GameObject HudContainer;

    [SerializeField]
    private Button StartBtn;
    [SerializeField]
    private Button LeaderBoardsBtn;
    [SerializeField]
    private Button AchievementsBtn;
    [SerializeField]
    private Button ExitBtn;
    [SerializeField]
    private Button HudButton;
    [SerializeField]
    private Button AuthorBtn;

    [SerializeField]
    private GameObject AuthorUI;
    [SerializeField]
    private Text MoneyText;



    private IGameController _gameController;
    [Inject]
    public void Init(IGameController gameController)
    {
        _gameController = gameController;
        _gameController.OnGameStateChange += HandleGameChange;
    }

    private void OnEnable()
    {
        if(_gameController.CurrentState == GameState.MENU){
            ShowMenu();
        }
    }
    private void OnDisable()
    {
        _gameController.OnGameStateChange -= HandleGameChange;
    }


    private void StartGame()
    {
        _gameController.ChangeGameState(GameState.PRE_GAME);
    }

    private void HandleGameChange(GameState gameState){
        Debug.Log(gameState.ToString());
        if(gameState == GameState.MENU){
            ShowMenu();
            HideHud();
        } else {
            HideMenu();
            ShowHud();
        }
    }
    private void ShowHud(){
        HudContainer.SetActive(true);
        HudButton.onClick.AddListener(PauseGame);
        
    }

    private void HideHud(){
        HudContainer.SetActive(false);
        HudButton.onClick.RemoveAllListeners();
    }

    private void PauseGame(){
        _gameController.ChangeGameState(GameState.MENU);
    }

    private void ShowMenu(){
        MoneyText.text = "Money: "+_gameController.Money;
        MenuContainer.SetActive(true);
        InitBtns();
    }

    private void HideMenu(){
        RemoveBtnListeners();
        MenuContainer.SetActive(false);
    }

    private void InitBtns(){
        RemoveBtnListeners();
        AddBtnListeners();
    }

    private void RemoveBtnListeners(){
        StartBtn.onClick.RemoveAllListeners();
        LeaderBoardsBtn.onClick.RemoveAllListeners();
        AchievementsBtn.onClick.RemoveAllListeners();
        ExitBtn.onClick.RemoveAllListeners();
    }

    private void AddBtnListeners(){
        StartBtn.onClick.AddListener(StartGame);
        LeaderBoardsBtn.onClick.AddListener(ShowLeaderboards);
        AchievementsBtn.onClick.AddListener(ShowAchievements);
        ExitBtn.onClick.AddListener(ExitGame);
        AuthorBtn.onClick.AddListener(ShowAuthors);
    }

    private void ShowAuthors() {
        AuthorUI.SetActive(true);
    }

    private void ShowLeaderboards(){
        _gameController.ShowLeaderboardsUI();
    }

    private void ShowAchievements(){
        _gameController.ShowAchievementsUI();
    }

    private void ExitGame(){
        Application.Quit();
    }
}
