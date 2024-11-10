using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<ApplicationSettings>().AsSingle();
    }
}