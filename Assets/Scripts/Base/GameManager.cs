using System;
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
    private List<PlotController> _plotControllerList;

    [SerializeField]
    private UIManager _uiManager;

    private void Awake() {
        _gamecontroller = new GameController();
        _inputcontroller = new InputController(_gamecontroller);
        _gamecontroller.OnPlayerDataChanged += OnPlayerDataChanged;
        _gamecontroller.OnPlotUpdated += OnPlotUpdated;
    }

    private void Start() {
        List<Plot> plots = _gamecontroller.GetAllPlots();
        int plotAvai = plots.Count;
        for(int i = 0; i < _plotControllerList.Count; i++)
        {
            if(plotAvai > 0)
            {
                _plotControllerList[i].InitialOwnedPlot(plots[i].Id, _gamecontroller, _inputcontroller);
                plotAvai--;
            }
            else
            {
                _plotControllerList[i].InitialLockedPlot(_gamecontroller, _inputcontroller);
            }
            
        }
        _uiManager?.Initialise(_gamecontroller, _inputcontroller);
        UpdateUI();
    }

    public void OnPlayerDataChanged(PlayerData playerData)
    {
        UpdateUI();
    }

    public void OnPlotUpdated(Plot plot)
    {

    }

    public void UpdateUI()
    {
        _uiManager?.UpdateAll();
    }

    private void Update() {
        _gamecontroller.Update();
        if(Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log(DataManager.Instance.SavePlayerData(_gamecontroller.GetPlayerData()));
        }
    }
    private void OnApplicationQuit() {
    }
}
