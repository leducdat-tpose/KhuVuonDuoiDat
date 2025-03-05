using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
public class GameController
{
    private Farm _farm;

    public event Action<Plot> OnPlotUpdated;
    public event Action<PlayerData> OnPlayerDataChanged;
    public GameController(int initCurrency, int initPlotCount)
    {
        _farm = new Farm(initCurrency, initPlotCount);
    }
    
    public void Update()
    {
        _farm.Update();
    }

    public List<Plot> GetAllPlots() => _farm.Plots;

    private Plot FindPlot(string plotId) 
        => _farm.Plots.FirstOrDefault(plot => plot.Id == plotId);

    public bool UsePlot<T>(string plotId, string itemId) where T: Item
    {
        Plot plot = FindPlot(plotId);
        if(plot == null) return false;
        if(_farm.PlayerData.Inventory[itemId] <= 0) return false;
        T item = DataManager.Instance.CreateItem<T>(itemId);
        if(item == null) return false;
        bool result = plot.StartFarm(item);
        if(result)
        {
            _farm.PlayerData.RemoveItemInInventory(item.Id, 1);
            OnPlotUpdated?.Invoke(plot);
            OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        }
        return result;
    }

    public bool BuyItem(string itemId, int amount)
    {
        bool result = _farm.BuyItem(itemId, amount);
        if(result)
        {
            OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        }
        return result;
    }

    public bool SellItem(string itemId, int amount)
    {
        bool result = _farm.SellItem(itemId, amount);
        if(result)
        {
            OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        }
        return result;
    }

    public PlayerData GetPlayerData() => _farm.PlayerData;

    public bool PlantItem(string plotId, string itemId)
    {
        Plot plot = FindPlot(plotId);
        Debug.Log($"Plot{plot.Id}");
        if(plot == null) return false;
        Debug.Log($"Inventory{_farm.PlayerData.Inventory[itemId] <= 0}");
        if(_farm.PlayerData.Inventory[itemId] <= 0) return false;
        Item item = DataManager.Instance.CreateItem(itemId);
        Debug.Log($"Item{item.Id}");
        if(item == null) return false;
        bool result = plot.StartFarm(item);
        Debug.Log($"Result{result}");
        if(result)
        {
            _farm.PlayerData.RemoveItemInInventory(itemId, 1);
            OnPlotUpdated?.Invoke(plot);
            OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        }
        return result;
    }

    public bool HarvestItem(string plotId)
    {
        Plot plot = FindPlot(plotId);
        if(plot == null || !plot.HaveProduct()) return false;
        Item item = DataManager.Instance.CreateItem(plot.CurrentObject.Product.ToString());
        if(item == null) return false;
        int amount = plot.Harvest();
        _farm.PlayerData.AddItemIntoInventory(item.Id, amount);
        OnPlotUpdated?.Invoke(plot);
        OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        return true;
    }

}