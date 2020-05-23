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
    public class BoardController : MonoBehaviour, IBoardController
    {
        [SerializeField]
        private Transform levelParentTransform;

        [SerializeField]
        private Transform ballSpawnPoint;

        [SerializeField]
        private GameObject ball;

        [SerializeField]
        private List<BumperController> bumpersOnLevel;

        private IGameController _gameController;
        private IInputService _input;
        private IPointsService _pointsService;
        private Vector3 initLevelPosition;
        private bool isNudging;

        [Inject]
        public void Init(IGameController gameController, IInputService input, IPointsService pointsService)
        {
            _gameController = gameController;
            _input = input;
            _pointsService = pointsService;
            initLevelPosition = levelParentTransform.position;
        }

        private void OnEnable() {
            _gameController.OnGameStateChange += HandleChange;
        }

        private void OnDisable() {
            _gameController.OnGameStateChange -= HandleChange;
        }

        private void HandleChange(GameState gameState){
            if(gameState == GameState.PRE_GAME){
                StartGame();
            } else if(gameState == GameState.MENU){
                EndGame();
            }
        }

        public void StartGame()
        {
            _gameController.ChangeGameState(GameState.PRE_GAME);

            EnableNudge();
            EnableBall();
            InitializeBumpers();
        }

        private void InitializeBumpers()
        {
            foreach(BumperController bumper in bumpersOnLevel)
            {
                if (bumper.Active)
                {
                    bumper.OnBump += BumperPointHandle;
                }
            }
        }

        private void BumperPointHandle(float value)
        {
            _pointsService.AddPoints(value);
        }

        public void EndGame()
        {
            DisableNudge();
            DisableBall();
            DisableBumpers();

            var reward = _pointsService.CalculateReward();
            _gameController.Money += (int)reward;
            _gameController.ChangeGameState(GameState.PRE_GAME);

            _pointsService.ResetPoints();
        }

        private void DisableBumpers()
        {
            foreach(var bumper in bumpersOnLevel)
            {
                bumper.OnBump -= BumperPointHandle;
            }
        }

        private void DisableBall()
        {
            ball.SetActive(false);
        }


        private void EnableBall()
        {
            ball.transform.position = ballSpawnPoint.position;
            ball.SetActive(true);
        }

        private void EnableNudge()
        {
            _input.OnNudge += Nudge;
        }

        private void DisableNudge()
        {
            _input.OnNudge -= Nudge;
        }

        private void Nudge()
        {
            if(isNudging == false)
            {
                isNudging = true;
                levelParentTransform.DOShakePosition(0.1f, new Vector3(0.2f, 0.1f, 0.2f), 1, 5, false, true)
                    .OnComplete(() => { isNudging = false; });
            }
        }

    }
}
