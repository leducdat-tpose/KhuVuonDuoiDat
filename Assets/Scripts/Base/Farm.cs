using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Farm
{
    public List<Plot> Plots{get;private set;}
    public PlayerData PlayerData{get;private set;}
    private DataManager _dataManager;
    public Farm(int initCurrency, int initPlotCount)
    {
        PlayerData = new PlayerData(initCurrency);
        Plots = new List<Plot>();
        _dataManager =DataManager.CreateAndInitialise();
        for(int i = 0; i < initPlotCount; i++)
        {
            Plots.Add(new Plot(id: $"plot_{i}"));
        }
    }

    public bool BuyItem(string id, int amount)
    {
        var item = _dataManager.GetItem(id);
        if(item == null) return false;
        int totalValue = item.Value * amount;
        if(!PlayerData.CanAfford(totalValue)) return false;
        PlayerData.SpendCurrency(totalValue);
        PlayerData.AddItemIntoInventory(item.Id, amount);
        return true; 
    }

    public bool SellItem(string id, int amount)
    {
        if(!PlayerData.Inventory.ContainsKey(id)) return false;
        if(PlayerData.Inventory[id] < amount) return false;
        var item = _dataManager.GetItem(id);
        if(item == null) return false;
        int totalValue = item.Value * amount;
        PlayerData.RemoveItemInInventory(item.Id, amount);
        PlayerData.AddCurrency(totalValue);
        return true;
    }

    public void BuyPlot(){}

    public void Update(){
        foreach(Plot plot in Plots) plot.Update();
    }
}
