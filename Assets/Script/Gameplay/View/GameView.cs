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
    [SerializeField] private Image _fill;
    [SerializeField] private Image _handle;
    [SerializeField] private Gradient _gradient;
    
    private PlayerModel _playerModel;
    private UIManager _uiManager;
    private ShopModel _shopModel;

    [Inject]
    public void Construct(PlayerModel playerModel, UIManager uiManager, ShopModel shopModel)
    {
        _playerModel = playerModel;
        _uiManager = uiManager;
        _shopModel = shopModel;

        uiManager.OnReload += Reload;
        _playerModel.OnTickInJump += IsTicked;
        _playerModel.OnScoreUpdate += UpdateScore;
    }
    
    private void Awake()
    {
        InjectService.Instance.Inject(this);
        Reload();
    }

    private void OnEnable()
    {
        _handle.sprite = _shopModel.GetSelectSkinImage();
    }

    private void UpdateScore(int score)
    {
        _score.text = score.ToString();
    }

    private void IsTicked(float value)
    {
        _slider.value = value;

        if(_fill != null)
            _fill.color = _gradient.Evaluate(value);
    }

    public void Reload()
    {
        _score.text = "0";
        _slider.value = 0.1f;
    }
}
