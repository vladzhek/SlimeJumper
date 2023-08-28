using System;
using Script.Gameplay;
using Script.Gameplay.Data;
using Script.Gameplay.Mono;
using Script.Gameplay.Player;
using Script.Gameplay.Progress;
using Script.Gameplay.Services;
using Script.Gameplay.View;
using UnityEngine;
using YG;
using Zenject;

public class SceneDataLoader : MonoBehaviour
{
    [SerializeField] private Transform _playerPositon;
    [SerializeField] private Transform _spawnPostion;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _sfx;

    private SpawnerModel _spawnerModel;
    private PlayerModel _playerModel;
    private StaticDataService _staticDataService;
    private SaveLoadService _saveLoadService;
    private CurrencyModel _currencyModel;
    private UIManager _uiManager;
    private TimeModel _timeModel;
    private ShopModel _shopModel;
    private SettingModel _settingModel;
    private AchievementModel _achievementModel;
    private AudioService _audioService;
    private AdsService _adsService;

    [Inject]
    public void Construct(SpawnerModel spawnerModel,
        StaticDataService staticDataService,
        SaveLoadService saveLoadService,
        CurrencyModel currencyModel,
        UIManager uiManager,
        TimeModel timeModel,
        ShopModel shopModel,
        SettingModel settingModel,
        AchievementModel achievementModel,
        AudioService audioService,
        AdsService adsService,
        PlayerModel playerModel)
    {
        _playerModel = playerModel;
        _spawnerModel = spawnerModel;
        _currencyModel = currencyModel;
        _saveLoadService = saveLoadService;
        _staticDataService = staticDataService;
        _uiManager = uiManager;
        _timeModel = timeModel;
        _shopModel = shopModel;
        _settingModel = settingModel;
        _audioService = audioService;
        _achievementModel = achievementModel;
        _adsService = adsService;
    }
    
    //--- Enter game
    public void Awake()
    {
        // Need First
        _staticDataService.Load();
        _saveLoadService.LoadProgress();
        _uiManager.Load();
        
        // TODO: Бабосы позже удалить
        //_currencyModel.AddCurrency(CurrencyType.Soft, 200);
        //_currencyModel.AddCurrency(CurrencyType.Hard, 200);

        _shopModel.Initialize();
        _playerModel.Initialize(_playerPositon.position);
        _spawnerModel.Initialize(_spawnPostion);
        _achievementModel.Initialize();
        _audioService.Initialize(_staticDataService, _sfx, _music);
        _adsService.Initialize();
        Subscribe();
    }
    
    public void Start()
    {
        _settingModel.Initialize();
        
        _uiManager.OpenWindow(WindowType.Menu);
        _uiManager.OpenWindow(WindowType.Header);
    }

    private void Subscribe()
    {
        _uiManager.OnReload += ReloadGame;
        _playerModel.OnDeath += Death;
    }

    private void Death()
    {
        _uiManager.OpenWindow(WindowType.Death);
    }

    private void ReloadGame()
    {
        _playerModel.ReloadPlayer();
        _spawnerModel.ClearObstacle();
        _spawnerModel.SetSpawnStatus(false);
        _adsService.ShowFullScreenBanner();
    }

    //--- Gameplay
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerModel.StartJump();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _playerModel.EndJump();
        }

        if (_spawnerModel.GetSpawnStatus())
        {
            _spawnerModel.SpawnObstacle();
        }
    }

    private void OnApplicationQuit()
    {
        _saveLoadService.SaveProgress();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            _saveLoadService.SaveProgress();
        }
    }
}
