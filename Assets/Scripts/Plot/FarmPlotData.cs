using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FarmPlotData
{
    public string PlotID;
    public Vector2Int GridPos;
    public float UnlockCost;
    public bool isUnlocked;
    public IFarmable CurrentSeed;
    // public float GrowthProgress;
    // public float GrowthRate;

    public bool CanPlant()
    {
        return isUnlocked && CurrentSeed == null;
    }

    public void Plant(IFarmable seed)
    {
        CurrentSeed = seed;
    }
    public void InitialiseDebug()
    {
        isUnlocked = true;
        CurrentSeed = new Seed();
        CurrentSeed.DurationGrowth = 3f;
    }

}
