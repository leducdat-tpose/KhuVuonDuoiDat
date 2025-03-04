using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot
{
    public string Id;
    public float UnlockCost;
    public bool isUnlocked;
    public IFarmable CurrentObject;

    public Plot(string id)
    {
        Id = id;
        UnlockCost = 100;
        isUnlocked = true;
        CurrentObject = null;
    }

    public void Update()
    {
        if(CurrentObject == null) return;
        CurrentObject.Update();
    }
    public bool StartFarm(Item item)
    {
        if(!isUnlocked) return false;
        if(CurrentObject != null) return false;
        if(item is not IFarmable farmableItem) return false;
        CurrentObject = farmableItem;
        return true;
    }
    public void Harvest()
    {
    }
    public void ResetPlot()
    {
        CurrentObject = null;
    }
    public bool HaveProduct()
    {
        if(CurrentObject == null) return false;
        if(CurrentObject.AmountProduct == 0) return false;
        return true;
    }
}