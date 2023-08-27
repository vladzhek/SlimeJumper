using Script.Gameplay;
using Script.Gameplay.Data.Game;
using Script.Gameplay.Mono;
using Script.Gameplay.Player;
using Script.Gameplay.Services;
using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private UIManager _uiManager;
        
    public override void InstallBindings()
    {
        InjectService.Instance.SetContainer(this);
        
        BindService();
        BindModel();
        BindPrefabs();
        BindTickables();
        
    }
    
    private void BindModel()
    {
        Container.Bind<SpawnerModel>().AsSingle();
        Container.Bind<PlayerModel>().AsSingle();
        Container.Bind<CurrencyModel>().AsSingle();
        Container.Bind<TimeModel>().AsSingle();
        Container.Bind<ShopModel>().AsSingle();
        Container.Bind<AchievementModel>().AsSingle();
        Container.Bind<SettingModel>().AsSingle();
    }
    
    private void BindService()
    {
        Container.Bind<StaticDataService>().AsSingle();
        Container.Bind<SaveLoadService>().AsSingle();
        Container.Bind<ProgressService>().AsSingle();
        Container.Bind<AudioService>().AsSingle();
        Container.Bind<InjectService>().AsSingle();
        Container.Bind<AdsService>().AsSingle();
    }

    private void BindPrefabs()
    {
        Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();
    }
    
    private void BindTickables()
    {
        Container.BindInterfacesAndSelfTo<PlayerJump>().AsSingle();
    }

    public void Inject(object obj)
    {
        Container.Inject(obj);
    }
}
