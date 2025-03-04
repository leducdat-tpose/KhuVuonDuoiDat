using System.Collections;
using System.Collections.Generic;

public class InputController
{
    private GameController _gameController;
    private string _selectedPlot;
    private string _selectedItem;

    public InputController(GameController gameController)
    {
        _gameController = gameController;
        _selectedItem = null;
        _selectedPlot = null;
    }
}
