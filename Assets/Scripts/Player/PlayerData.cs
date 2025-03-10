using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class PlayerData
{
    [JsonProperty]
    public int Currency{get; private set;}
    [JsonProperty]
    public Dictionary<string, int> Inventory{get; private set;} = new Dictionary<string, int>();
    [JsonProperty]
    public List<Plot> Plots{get; private set;} = new List<Plot>();
    [JsonProperty]
    public int ToolLevel{get; private set;}
    [JsonProperty]
    public int NumHiredWorker{get;private set;}
    
    public DateTime LastPlayedTime{get; private set;} = DateTime.Now;

    public static PlayerData CreateAndInit()
    {
        PlayerData playerData = new PlayerData();
        playerData.Initialise();
        return playerData;
    }

    private void Initialise()
    {
        Currency = Constant.initCurrency;
        for(int i = 0; i < Constant.InitNumPlot; i++)
        {
            Plots.Add(new Plot(id: $"plot_{i}"));
        }
        AddItemIntoInventory("TomatoSeed", 10);
        AddItemIntoInventory("BlueberrySeed", 10);
        AddItemIntoInventory("Cow", 2);
        // AddItemIntoInventory("Milk", 2);
        // AddItemIntoInventory("StrawberrySeed", 2);
        // AddItemIntoInventory("Strawberry", 2);
        // AddItemIntoInventory("Tomato", 2);
        // AddItemIntoInventory("Blueberry", 2);

        ToolLevel = Constant.InitToolLevel;
        NumHiredWorker = Constant.InitNumWorker;
    }

    public void AddItemIntoInventory(string id, int amount)
    {
        if(!Inventory.ContainsKey(id))
        {
            Inventory[id] = 0;
        }
        Inventory[id] += amount;
    }

    public bool RemoveItemInInventory(string id, int amount)
    {
        if(!Inventory.ContainsKey(id)) return false;
        if(Inventory[id] < amount) return false;
        Inventory[id] -= amount;
        if(Inventory[id] == 0) Inventory.Remove(id);
        return true;
    }

    public int GetAmountOfItemInInventory(string id)
    {
        if(Inventory.TryGetValue(id, out int amount)) return amount;
        return 0;
    }

    public void AddCurrency(int amount) => Currency += amount;

    public bool SpendCurrency(int amount)
    {
        if(!CanAfford(amount)) return false;
        Currency -= amount; return true;
    }
    public bool CanAfford(int cost) => Currency >= cost;
    public void SetLastPlayedTime(DateTime time)
    {
        LastPlayedTime = time;
    }
    public bool HavePlot(string plotId)
    {
        foreach(Plot plot in Plots)
        {
            if(plot.Id == plotId)
            {
                return false;
            }
        }
        return true;
    }

    public bool AddPlot(Plot plot)
    {
        Plots.Add(plot);
        return true;
    }
    public void HireWorker()
    {
        NumHiredWorker++;
    }
    public void UpgradeTool()
    {
        ToolLevel++;
    }

}
