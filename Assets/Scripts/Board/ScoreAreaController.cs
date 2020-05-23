using Assets.Scripts.Board;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreAreaController : MonoBehaviour
{
    [SerializeField]
    private string BallTag = "Ball";
    private IBoardController _boardController;

    [Inject]
    public void Init(IBoardController boardController)
    {
        _boardController = boardController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(BallTag))
        {
            _boardController.RestartGame();
        }
    }
}
