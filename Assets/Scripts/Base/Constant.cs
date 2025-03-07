using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    public static readonly string spriteFolderPath = "Sprite";
    public static readonly string csvFileName = "seeds.csv";
    public static readonly int MaxPlotCount = 9;
    public static readonly int numSeedBuy = 10;
    public static readonly int numAnimalBuy = 1;
    #region PlayerData
    public static readonly string PlayerDataJsonFileName = "playerdata.json";
    public static readonly int initCurrency = 500;
    public static readonly int InitNumPlot = 3;
    public static readonly int InitToolLevel = 1;
    public static readonly float ToolEfficient = 0.1f;
    public static readonly int InitNumWorker = 1;
    public static readonly double DurationWorkingWorker = 10;
    #endregion
    public static readonly string BlueberrySprite = "Sprite/blueberry";
    public static readonly string StrawberrySprite = "Sprite/strawberry";
    public static readonly string TomatoSprite = "Sprite/tomato";
    public static readonly string PlantSprite = "Sprite/smallplant";
}
