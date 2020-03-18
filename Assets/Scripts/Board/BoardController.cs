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
        private Transform ballSpawnPoint;

        [SerializeField]
        private GameObject ball;

        private IGameController _gameController;

        [Inject]
        public void Init(IGameController gameController)
        {
            _gameController = gameController;
        }

        public void EndGame()
        {
            _gameController.ChangeGameState(GameState.PRE_GAME);
            ball.SetActive(false);
            Invoke("StartGame", 5f); // Debug only
        }

        public void StartGame()
        {
            _gameController.ChangeGameState(GameState.IN_GAME);
            ball.transform.position = ballSpawnPoint.position;
            ball.SetActive(true);
        }
    }
}
