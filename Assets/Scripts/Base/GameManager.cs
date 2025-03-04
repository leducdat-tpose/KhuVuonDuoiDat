using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int initCurrency = 1000;
    [SerializeField]
    private int initPlotCount = 1;
    private GameController _gamecontroller;
    private InputController _inputcontroller;

    [SerializeField]
    private UIManager _uiManager;

    private void Awake() {
        _gamecontroller = new GameController(initCurrency, initPlotCount);
        _inputcontroller = new InputController(_gamecontroller);
        _gamecontroller.OnPlayerDataChanged += OnPlayerDataChanged;
        _gamecontroller.OnPlotUpdated += OnPlotUpdated;
    }

    private void Start() {
        _uiManager?.Initialise(_gamecontroller, _inputcontroller);
        UpdateUI();
    }

    public void OnPlayerDataChanged(PlayerData playerData)
    {}

    public void OnPlotUpdated(Plot plot)
    {}

    public void UpdateUI()
    {
        _uiManager?.UpdateAll();
    }
}
