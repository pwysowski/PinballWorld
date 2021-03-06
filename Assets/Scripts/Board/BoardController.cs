﻿using Assets.Scripts.Input;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
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

        [SerializeField]
        private FloatAreaController floatArea;
        [SerializeField]
        private Text scoreTxt;

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

        private void OnEnable()
        {
            _gameController.OnGameStateChange += HandleChange;
        }

        private void OnDisable()
        {
            _gameController.OnGameStateChange -= HandleChange;
        }

        private void HandleChange(GameState gameState)
        {
            if (gameState == GameState.PRE_GAME)
            {
                StartGame();
            }
            else if (gameState == GameState.MENU)
            {
                EndGame();
            }
        }

        public void StartGame()
        {
            EnableNudge();
            EnableBall();
            InitializeBumpers();
            InitializeFloatArea();
            
            scoreTxt.text = "SCORE: " + _pointsService.GetPoints();
        }

        private void InitializeFloatArea()
        {
            floatArea.OnPointsGained += BumperPointHandle;
        }

        private void InitializeBumpers()
        {
            foreach (BumperController bumper in bumpersOnLevel)
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
            scoreTxt.text = "SCORE: " + _pointsService.GetPoints();
        }

        public void EndGame()
        {
            DisableNudge();
            DisableBall();
            DisableBumpers();

            var reward = _pointsService.CalculateReward();
            _gameController.AddMoney((int)reward);
            _gameController.SaveGamepoints((int)reward);

            if (_gameController.FirstGamePlayed == false)
            {
                _gameController.FirstGamePlayed = true;
                _gameController.CompletedFirstGame();
            }

            _pointsService.ResetPoints();
        }

        public void RestartGame()
        {
            EndGame();
            _gameController.ChangeGameState(GameState.PRE_GAME);
        }

        private void DisableBumpers()
        {
            foreach (var bumper in bumpersOnLevel)
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
            if (isNudging == false)
            {
                isNudging = true;
                levelParentTransform.DOShakePosition(0.1f, new Vector3(0.2f, 0.1f, 0.2f), 1, 5, false, true)
                    .OnComplete(() => { isNudging = false; });
            }
        }

    }
}
