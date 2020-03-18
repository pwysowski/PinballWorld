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

        private IGameController _gameController;
        private IInputService _input;
        private Sequence seq;
        private bool isNudging;
        private Vector3 initLevelPosition;

        [Inject]
        public void Init(IGameController gameController, IInputService input)
        {
            _gameController = gameController;
            _input = input;

            initLevelPosition = levelParentTransform.position;
        }

        public void EndGame()
        {
            DisableNudge();
            _gameController.ChangeGameState(GameState.PRE_GAME);
            ball.SetActive(false);
            Invoke("StartGame", 5f); // Debug only
        }
        public void StartGame()
        {
            _gameController.ChangeGameState(GameState.IN_GAME);

            EnableNudge();

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
