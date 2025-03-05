using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private GameController _gameController;
    private string _selectedPlotId;
    private string _selectedItemId;

    private ToolType _toolType;

    public enum ToolType{
        None,
        Plant,
        Harvest,
    }

    public InputController(GameController gameController)
    {
        _gameController = gameController;
        _selectedItemId = null;
        _selectedPlotId = null;
        _toolType = ToolType.None;
    }

    public void SelectPlot(string PlotId)
    {
        _selectedPlotId = PlotId;
        ApplyItemToPlot();
    }
    public void SelectItem(string itemId)
    {
        _selectedItemId = itemId;
    }

    public void SelectTool(ToolType toolType)
    {
        _toolType = toolType;
    }

    private void ResetValue()
    {
        _selectedItemId = null;
        _selectedPlotId = null;
        _toolType = ToolType.None;
    }

    private void ApplyItemToPlot()
    {
        if(string.IsNullOrEmpty(_selectedItemId)) return;
        switch (_toolType)
        {
            case ToolType.Plant:
            Debug.Log($"Plant item{_selectedPlotId}, {_selectedItemId}");
                _gameController.PlantItem(_selectedPlotId, _selectedItemId);
                ResetValue();
                break;
            case ToolType.Harvest:
                _gameController.HarvestItem(_selectedPlotId);
                ResetValue();
                break;
        }
    }
}
