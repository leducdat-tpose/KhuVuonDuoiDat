using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class Plot
{
    [JsonProperty]
    public string Id{get; private set;}
    [JsonProperty]
    public int UnlockCost{get; private set;}
    [JsonProperty]
    public bool isUnlocked{get; private set;}
    [JsonProperty]
    public FarmableItem CurrentItem{get; private set;}
    public Plot(string id)
    {
        Id = id;
        UnlockCost = Constant.PayValueUnlockPlot;
        isUnlocked = true;
        CurrentItem = null;
    }

    public void Update()
    {
        if(CurrentItem == null) return;
        CurrentItem.Update();
    }
    public bool StartFarm(FarmableItem item)
    {
        if(!isUnlocked) return false;
        if(CurrentItem != null) return false;
        CurrentItem = item;
        CurrentItem.StartGrowing();
        return true;
    }
    public int Harvest()
    {
        int amount = CurrentItem.CollectProduct();
        if(CurrentItem.IsOutOfProduct()) ResetPlot();
        return amount;
    }
    public void ResetPlot()
    {
        CurrentItem = null;
    }
    public bool HaveProduct()
    {
        if(CurrentItem == null) return false;
        if(CurrentItem.AmountProduct == 0) return false;
        return true;
    }
    public void UnlockPlot() => isUnlocked = true;
}