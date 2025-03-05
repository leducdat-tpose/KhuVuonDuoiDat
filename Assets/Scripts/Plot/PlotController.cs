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
    public void InitialOwnedPlot(string Id, GameController gameController, InputController inputController)
    {
        Initialise(Id);
        _inputcontroller = inputController;
        _gameController = gameController;
        _gameController.OnPlotUpdated += OnPlotUpdated;
        _harvestBtn.onClick.AddListener(() => _gameController.HarvestItem(Id));
        
    }

    public void InitialLockedPlot(GameController gameController, InputController inputController)
    {
        _plotRender = GetComponent<SpriteRenderer>();
        _plotRender.color = Color.grey;
        _plotRender.sprite = _spriteLocked;
        _inputcontroller = inputController;
        gameController.OnPlotUpdated += OnPlotUpdated;
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

    public void Initialise(string Id)
    {
        this.Id = Id;
        _plotRender = GetComponent<SpriteRenderer>();
        ResetPlotController();
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
        if(plot.CurrentObject != null)
        {
            SetSpriteProduct(plot.CurrentObject);
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
        if(_plot.CurrentObject == null) return;
        _timeGrowthText.text = $"{_plot.CurrentObject.GetTimeCountGrowth()}s";
        _amountProductText.text = $"x{_plot.CurrentObject.AmountProduct}";
        if(_plot.HaveProduct())
        {
            SetActiveHarvestBtn(true);
        }
        else SetActiveHarvestBtn(false);
    }

    private void SetSpriteProduct(IFarmable farmableItem)
    {
        if(_spriteHaveProduct != null) return;
        Item item = (Item)farmableItem;
        _spriteHaveProduct = DataManager.Instance?.GetItemSprite(item.Id);
    }
    private void SetActiveHarvestBtn(bool option)
    {
        if(_harvestBtn.gameObject.activeSelf == option) return;
        _harvestBtn.gameObject.SetActive(option);
    }
}
