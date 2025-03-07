using System;
using System.Collections;
using System.Collections.Generic;

public static class Constant
{
    public static readonly string spriteFolderPath = "Sprite";
    public static readonly string FileItemConfig = "ItemConfig.csv";
    public static readonly int MaxPlotCount = 9;
    public static readonly int numSeedBuy = 10;
    public static readonly int numAnimalBuy = 1;
    
    #region PlayerData
    public static readonly string PlayerDataJsonFileName = "playerdata.json";
    public static readonly int initCurrency = 0;
    public static readonly int InitNumPlot = 3;
    public static readonly int InitToolLevel = 1;
    public static readonly float ToolEfficient = 0.1f;
    public static readonly int PayValueUpgradeTool = 500;
    public static readonly int InitNumWorker = 1;
    public static readonly double DurationWorkingWorker = 2*60;
    public static readonly int PayValueForWorker = 500;
    #endregion

    #region Plot
    public static readonly int PayValueUnlockPlot = 500;
    #endregion
}
