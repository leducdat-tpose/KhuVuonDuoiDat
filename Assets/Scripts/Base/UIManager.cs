using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameController _gameController;
    private InputController _inputController;

    [Header("References")]
    [SerializeField]
    private GameObject _currencyPanel;
    [SerializeField]
    private GameObject _workerCountPanel;
    [SerializeField]
    private GameObject _plotCountPanel;
    [SerializeField]
    private GameObject _toolLevelPanel;
    [SerializeField]
    private GameObject _inventoryPanel;
    [SerializeField]
    private TextMeshProUGUI _currencyText;
    [SerializeField]
    private Button _plantItemBtn;
    [SerializeField]
    private Button _harvestItemBtn;
    [SerializeField]
    private Button _storeBtn;
    [SerializeField]
    private Button _inventoryBtn;
    [SerializeField]
    private Button _closeInvenBtn;

    public void Initialise(GameController gameController, InputController inputController)
    {
        _gameController = gameController;
        _inputController = inputController;
        InitialiseBottomPanel();
    }

    private void InitialiseBottomPanel()
    {
        _plantItemBtn.onClick.AddListener(() => _inputController.SelectTool(InputController.ToolType.Plant));
        _harvestItemBtn.onClick.AddListener(() => _inputController.SelectTool(InputController.ToolType.Harvest));
        _inventoryBtn.onClick.AddListener(() => SetInventoryPanel(true));
        _inventoryPanel.transform.GetComponent<Inventory>().Initialise(_gameController, _inputController);
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.B))
        {
            _gameController.BuyItem("TomatoSeed", 10);
        }
    }

    public void UpdateAll()
    {
        if(_currencyPanel != null)
        {
            UpdateCurrencyPanel();
        }
    }

    public void UpdateCurrencyPanel()
    {
        PlayerData playerData = _gameController.GetPlayerData();
        if(_currencyText != null) _currencyText.text = $"Currency: {playerData.Currency}";
        if(_workerCountPanel != null)
        {
            _workerCountPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{playerData.IdleWorker}/{playerData.HiredWorker}";
        }
        if(_plotCountPanel != null)
        {
            _plotCountPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"Plots: {_gameController.GetAllPlots().Count}";
        }
        if(_toolLevelPanel != null)
        {
            _toolLevelPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"Lvl.{playerData.ToolLevel}";
        }
    }

    private void OnBuyItemBtn(string itemId)
    {
        _gameController.BuyItem(itemId, 10);
    }
    private void OnSellItemBtn(string itemId)
    {
        _gameController.SellItem(itemId, 1);
    }

    private void SetInventoryPanel(bool option)
    {
        _inventoryPanel.SetActive(option);
    }

    // private void OnGUI() {
    //     if(GUI.Button(new Rect(10, 10, 70, 30), "Test 1"))
    //     {
    //         TestFunc1();
    //     }
    //     if(GUI.Button(new Rect(10, 40, 70, 30), "Test 2"))
    //     {
    //         TestFunc2();
    //     }
    // }
}
