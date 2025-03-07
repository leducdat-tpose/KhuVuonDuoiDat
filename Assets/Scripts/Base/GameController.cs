using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class GameController
{
    private Farm _farm;

    public event Action<Plot> OnPlotUpdated;
    public event Action<PlayerData> OnPlayerDataChanged;
    public GameController()
    {
        _farm = new Farm();
    }
    
    public void Update()
    {
        _farm.Update();
    }

    public List<Plot> GetAllPlots() => _farm.Plots;

    public Plot FindPlot(string plotId) 
        => _farm.Plots.FirstOrDefault(plot => plot.Id == plotId);

    public bool UsePlot<T>(string plotId, string itemId) where T: FarmableItem
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

    public bool BuyPlot(string plotId)
    {
        Plot plot = _farm.BuyPlot(plotId);
        if(plot == null) return false;
        OnPlotUpdated?.Invoke(plot);
        OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        return true;
    }

    public bool BuyItem(string itemId, int amount = 0)
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
        if(plot == null) return false;
        if(_farm.PlayerData.Inventory[itemId] <= 0) return false;
        FarmableItem item = DataManager.Instance.CreateItem<FarmableItem>(itemId);
        if(item == null) return false;
        bool result = plot.StartFarm(item);
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
        Item item = DataManager.Instance.CreateItem(plot.CurrentItem.Product.ToString());
        if(item == null) return false;
        int amount = plot.Harvest();
        _farm.PlayerData.AddItemIntoInventory(item.Id, amount);
        OnPlotUpdated?.Invoke(plot);
        OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        return true;
    }

}