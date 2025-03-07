using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private GameController _gameController;
    private string _selectedPlotId;
    private string _selectedItemId;

    private Command _command;

    public enum Command{
        None,
        Plant,
        Harvest,
        Buy,
        Sell,
    }

    public InputController(GameController gameController)
    {
        _gameController = gameController;
        _selectedItemId = null;
        _selectedPlotId = null;
        _command = Command.None;
    }

    public void SelectPlot(string PlotId)
    {
        _selectedPlotId = PlotId;
        ProgressInput();
    }
    public void SelectItem(string itemId)
    {
        _selectedItemId = itemId;
    }

    public void SelectCommand(Command commandType)
    {
        _command = commandType;
        ProgressInput();
    }

    public void ResetValue()
    {
        _selectedItemId = null;
        _selectedPlotId = null;
        _command = Command.None;
    }

    private void ProgressInput()
    {
        if(string.IsNullOrEmpty(_selectedItemId)) return;
        switch (_command)
        {
            case Command.Plant:
                if(string.IsNullOrEmpty(_selectedPlotId)) break;
                _gameController.PlantItem(_selectedPlotId, _selectedItemId);
                ResetValue();
                break;
            case Command.Harvest:
                if(string.IsNullOrEmpty(_selectedPlotId)) break;
                _gameController.HarvestItem(_selectedPlotId);
                ResetValue();
                break;
            case Command.Buy:
                _gameController.BuyItem(_selectedItemId);
                break;
            case Command.Sell:
                SellItemSelected();
                break;
        }
    }
    public void SellItemSelected()
    {
        if(string.IsNullOrEmpty(_selectedItemId)) return;
        bool result = _gameController.SellItem(_selectedItemId, _gameController.GetPlayerData().GetAmountOfItemInInventory(_selectedItemId));
        ResetValue();
    }
}
