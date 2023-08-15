using System;
using Script.Gameplay;
using Script.Gameplay.Mono;
using Script.Gameplay.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameView : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Slider _slider;
    
    private PlayerModel _playerModel;
    private UIManager _uiManager;

    [Inject]
    public void Construct(PlayerModel playerModel, UIManager uiManager)
    {
        _playerModel = playerModel;
        _uiManager = uiManager;

        uiManager.OnReload += Reload;
        _playerModel.OnTickInJump += IsTicked;
        _playerModel.OnScoreUpdate += UpdateScore;
    }
    
    private void Awake()
    {
        InjectService.Instance.Inject(this);
        Reload();
    }

    private void UpdateScore(int score)
    {
        _score.text = score.ToString();
    }

    private void IsTicked(float value)
    {
        //TODO: Сделать 3 диапазона цветов и применить к slider
        _slider.value = value;
    }

    public void Reload()
    {
        _score.text = "0";
        _slider.value = 0f;
    }
}
