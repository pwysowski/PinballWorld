using Assets.Scripts.Input;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Board
{
    public class StartGameTrigger : MonoBehaviour
    {    
    [SerializeField]
    private string BallTag = "Ball";
    private bool sentry = false;
    private IGameController _gameController;

    [Inject]
    public void Init(IGameController gameController)
    {
        _gameController = gameController;
    }

    private void OnEnable(){
        _gameController.OnGameStateChange += HandleChange;
    }

    private void OnDisable(){
        _gameController.OnGameStateChange -= HandleChange;
    }

    private void HandleChange(GameState gameState){
        if(gameState == GameState.PRE_GAME){
            sentry = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(BallTag))
        {
            _gameController.ChangeGameState(GameState.IN_GAME);
            sentry = true;
        }
    }
    }
}