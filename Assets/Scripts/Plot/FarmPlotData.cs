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

    public bool Plant(Seed seed)
    {
        if(!isUnlocked) return false;
        if(!GameController.Instance.PlayerData.UseItemInInventory(seed.Name, 1)) return false;
        CurrentSeed = seed;
        return true;
    }
    public void InitialiseDebug()
    {
        isUnlocked = true;
    }

}
