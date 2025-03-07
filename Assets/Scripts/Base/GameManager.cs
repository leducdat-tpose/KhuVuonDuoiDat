using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private GameController _gamecontroller;
    private InputController _inputcontroller;

    [SerializeField]
    private GameObject _plotsContain;
    private List<PlotController> _plotControllerList = new List<PlotController>();

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
        foreach(Transform plot in  _plotsContain.transform)
        {
            if(plot.TryGetComponent<PlotController>(out PlotController plotController))
            {
                _plotControllerList.Add(plotController);
            }
        }
        for(int i = 0; i < _plotControllerList.Count; i++)
        {
            if(plotAvai > 0)
            {
                _plotControllerList[i].InitialPlot(plots[i].Id, _gamecontroller, _inputcontroller, isUnlocked:true);
                plotAvai--;
            }
            else
            {
                _plotControllerList[i].InitialPlot($"plot_{i}", _gamecontroller, _inputcontroller, isUnlocked:false);
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
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            DataManager.Instance.DebugDataCSV();
        }
    }

    private void OnApplicationQuit() {
        DataManager.Instance?.SavePlayerData(_gamecontroller.GetPlayerData());
    }
}

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor:Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}