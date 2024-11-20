using Squad;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private BrawlersScriptable brawlersScriptable;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<ApplicationSettings>().AsSingle();
        Container.Bind<BrawlersScriptable>().FromInstance(brawlersScriptable).AsSingle().NonLazy();
    }
}