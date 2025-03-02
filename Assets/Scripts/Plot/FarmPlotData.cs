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
    public Item CurrentItem{get; private set;}
    // public float GrowthProgress;
    // public float GrowthRate;

    public bool CanFarm() => isUnlocked && CurrentItem == null;

    public bool Farming(Item item)
    {
        if(item.Type != ItemType.Seed && item.Type != ItemType.Animal) return false;
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
