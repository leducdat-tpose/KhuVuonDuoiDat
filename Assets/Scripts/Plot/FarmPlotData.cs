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
    public Seed CurrentSeed;
    // public float GrowthProgress;
    // public float GrowthRate;
    
    public bool CanPlant()
    {
        return isUnlocked && CurrentSeed != null;
    }

    public void Plant(Seed seed)
    {
        CurrentSeed = seed;
    }
    public void InitialiseDebug()
    {
        isUnlocked = true;
    }

}
