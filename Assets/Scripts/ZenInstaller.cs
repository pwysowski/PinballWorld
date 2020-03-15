using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZenInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGameController>().To<GameController>().AsSingle();
    }
}
