using Coins;
using Squad;
using Squad.UI;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private BrawlersScriptable brawlersScriptable;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<ApplicationSettings>().AsSingle();
        Container.Bind<BrawlersScriptable>().FromInstance(brawlersScriptable).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CoinsCounter>().AsSingle();
        Container.Bind<CoinsUI>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BrawlerSpawnUI>().FromComponentsInHierarchy().AsCached();
    }
}