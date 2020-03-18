using Assets.Scripts.Board;
using Assets.Scripts.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZenInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputService>().To<InputService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IBoardController>().To<BoardController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGameController>().To<GameController>().AsSingle();
    }
}
