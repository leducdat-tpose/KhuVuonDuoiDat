using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Farm
{
    public List<Plot> Plots{get;private set;}
    public PlayerData PlayerData{get;private set;}
    private DataManager _dataManager;
    public Farm()
    {
        _dataManager = DataManager.CreateAndInitialise();
        // PlayerData = PlayerData.CreateAndInit();
        PlayerData = _dataManager.LoadPlayerData();
        Plots = PlayerData.Plots;
    }

    public bool BuyItem(string id, int amount = 0)
    {
        var item = _dataManager.GetItem(id);
        if(item == null) return false;
        if(amount == 0)
        {
            if(item.Type == ItemType.Animal) 
                amount = Constant.numAnimalBuy;
            else if(item.Type == ItemType.Seed) 
                amount = Constant.numSeedBuy;
            else amount = 1;
        }
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

    public Plot BuyPlot(string plotId){
        if(!PlayerData.HavePlot(plotId)) return null;
        Plot plot = new Plot(plotId);
        if(!PlayerData.SpendCurrency(plot.UnlockCost)) return null;
        plot.UnlockPlot();
        PlayerData.AddPlot(plot);
        return plot;
    }

    public void Update(){
        foreach(Plot plot in Plots) plot.Update();
    }
}
