using UnityEngine;
using Zenject;

public class GameRunner : MonoInstaller
{
    [SerializeField] private Craft _craft;
    [SerializeField] private ResourcePool _resources;

    public override void InstallBindings()
    {
        Container.Bind<Craft>().FromInstance(_craft).AsSingle();
        Container.Bind<ResourcePool>().FromInstance(_resources).AsSingle();
    }
}
