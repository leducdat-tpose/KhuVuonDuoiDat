using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlotController : MonoBehaviour
{
    public string Id{get;private set;}
    [SerializeField]
    private SpriteRenderer _plotRender;
    [SerializeField]
    private Sprite _spriteEmpty;
    private Sprite _spriteHaveProduct;
    [SerializeField]
    private Sprite _spriteLocked;
    private GameController _gameController;
    private InputController _inputcontroller;
    [SerializeField]
    private TextMeshProUGUI _amountProductText;
    [SerializeField]
    private TextMeshProUGUI _workerProductText;
    private Plot _plot;
    [SerializeField]
    private Button _harvestBtn;
    [SerializeField]
    private TextMeshProUGUI _timeGrowthText;
    public void Initialise(string Id)
    {
        this.Id = Id;
        _plotRender = GetComponent<SpriteRenderer>();
        ResetPlotController();
    }
    public void InitialPlot(string Id, GameController gameController, InputController inputController, bool isUnlocked = false)
    {
        Initialise(Id);
        _inputcontroller = inputController;
        _gameController = gameController;
        _gameController.OnPlotUpdated += OnPlotUpdated;
        _harvestBtn.onClick.AddListener(() => {
            _gameController.HarvestItem(Id);
            SetActiveHarvestBtn(false);
            _amountProductText.text = $"x{0}";
        });
        if(isUnlocked == false)
        {
            _plotRender.sprite = _spriteLocked;
        }
        else
        {
            _plot = _gameController.FindPlot(Id);
            UpdateVisual(_plot);
        }
    }
    private void Start() {
        SetActiveHarvestBtn(false);
    }

    public void OnPlotUpdated(Plot plot)
    {
        if(plot.Id != Id) return;
        _plot = plot;
        UpdateVisual(plot);
    }
    private void ResetPlotController()
    {
        _plotRender.sprite = _spriteEmpty;
        _spriteHaveProduct = null;
    }
    private void UpdateVisual(Plot plot)
    {
        if(!plot.isUnlocked)
        {
            _plotRender.sprite = _spriteLocked;
            return;
        }
        if(plot.CurrentItem != null)
        {
            SetSpriteProduct(plot.CurrentItem);
            _plotRender.sprite = _spriteHaveProduct;
        }
        else
        {
            _plotRender.sprite = _spriteEmpty;
            _plot = null;
            _timeGrowthText.text = 0.ToString();
        }
    }
    private void OnMouseDown() {
        _inputcontroller.SelectTool(InputController.ToolType.Plant);
        _inputcontroller.SelectPlot(Id);
    }

    private void Update() {
        if(_plot == null) return;
        if(_plot.CurrentItem == null) return;
        _timeGrowthText.text = $"{_plot.CurrentItem.GetTimeCountGrowth()}s";
        _amountProductText.text = $"x{_plot.CurrentItem.AmountProduct}";
        if(_plot.HaveProduct())
        {
            SetActiveHarvestBtn(true);
        }
        else SetActiveHarvestBtn(false);
    }

    private void SetSpriteProduct(FarmableItem farmableItem)
    {
        if(_spriteHaveProduct != null) return;
        _spriteHaveProduct = DataManager.Instance?.GetItemSprite(farmableItem.Id);
    }
    private void SetActiveHarvestBtn(bool option)
    {
        if(_harvestBtn.gameObject.activeSelf == option) return;
        _harvestBtn.gameObject.SetActive(option);
    }
}
