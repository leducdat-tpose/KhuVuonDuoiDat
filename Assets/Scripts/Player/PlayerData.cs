using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    public int Currency{get; private set;}
    public Dictionary<string, int> Inventory{get; private set;}
    public List<Plot> Plots{get; private set;}
    public int ToolLevel{get; private set;}
    public int HiredWorker{get;private set;}
    public int IdleWorker{get;private set;}
    public DateTime LastPlayedTime{get; private set;}
    public PlayerData()
    {
        Currency = Constant.initCurrency;
        Plots = new List<Plot>();
        for(int i = 0; i < Constant.initPlotCount; i++)
        {
            Plots.Add(new Plot(id: $"plot_{i}"));
        }
        Inventory = new Dictionary<string, int>();
        ToolLevel = 2;
        HiredWorker = 4;
        IdleWorker = HiredWorker;
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

    public void AddCurrency(int amount) => Currency += amount;

    public bool SpendCurrency(int amount)
    {
        if(!CanAfford(amount)) return false;
        Currency -= amount; return true;
    }
    public bool CanAfford(int cost) => Currency >= cost;
}
