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

    [Header("Panel")]
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
    private GameObject _storePanel;
    [Header("Button")]
    [SerializeField]
    private Button _plantItemBtn;
    [SerializeField]
    private Button _upHireWorkerBtn;
    [SerializeField]
    private Button _upToolLvlBtn;
    [SerializeField]
    private Button _storeBtn;
    [SerializeField]
    private Button _inventoryBtn;

    public void Initialise(GameController gameController, InputController inputController)
    {
        _gameController = gameController;
        _inputController = inputController;
        InitialiseTopPanel();
        InitialiseBottomPanel();
        InitialiseStorePanel();
        InitialiseInventoryPanel();
    }

    private void InitialiseTopPanel()
    {
        _upHireWorkerBtn.onClick.AddListener(() => {
            _gameController.HireWorker();
            UpgradeTopPanel();
        });
        _upToolLvlBtn.onClick.AddListener(() => _gameController.UpgradeTool());
    }

    private void InitialiseBottomPanel()
    {
        _plantItemBtn.onClick.AddListener(() => _inputController.SelectCommand(InputController.Command.Plant));
    }

    private void InitialiseInventoryPanel()
    {
        if(_inventoryBtn == null || _inventoryPanel == null) return;
        _inventoryPanel.transform.GetComponent<Inventory>().Initialise(_gameController, _inputController);
        _inventoryBtn.onClick.AddListener(() => _inventoryPanel.gameObject.SetActive(true));
        _inventoryPanel.SetActive(false);
    }

    private void InitialiseStorePanel()
    {
        if(_storePanel == null || _storeBtn == null) return;
        _storePanel.GetComponent<Store>().Initialise(_gameController, _inputController);
        _storeBtn.onClick.AddListener(() => _storePanel.gameObject.SetActive(true));
        _storePanel.SetActive(false);
    }

    public void UpdateAll()
    {
        UpgradeTopPanel();
    }

    public void UpgradeTopPanel()
    {
        PlayerData playerData = _gameController.GetPlayerData();
        if(_currencyPanel != null)
        {
            _currencyPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"Currency: {playerData.Currency}";
        }
        if(_workerCountPanel != null)
        {
            _workerCountPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"Worker: {_gameController.GetNumIdleWorker()}/{playerData.NumHiredWorker}";
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
}
