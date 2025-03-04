using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    public int Currency{get; private set;}
    public Dictionary<string, int> Inventory{get; private set;}
    public PlayerData(int initCurrency)
    {
        Currency = initCurrency;
        Inventory = new Dictionary<string, int>();
        foreach(ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            Inventory[type.ToString()] = 0;
        }
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
