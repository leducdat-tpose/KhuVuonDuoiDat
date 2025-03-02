using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class FarmPlotData
{
    public string PlotID;
    public float UnlockCost;
    public bool isUnlocked;
    public IFarmable CurrentItem{get; private set;}
    // public float GrowthProgress;
    // public float GrowthRate;

    public bool CanFarm() => isUnlocked && CurrentItem == null;

    public bool Farming(IFarmable item)
    {
        CurrentItem = item;
        return true;
    }
    public void InitialiseDebug()
    {
        isUnlocked = true;
    }

    public void ResetPlot()
    {
        CurrentItem = null;
    }

}
