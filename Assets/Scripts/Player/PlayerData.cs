using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int Currency{get; private set;}
    public Dictionary<string, int> Inventory{get; private set;}
    public PlayerData()
    {
        Currency = 0;
        Inventory = new Dictionary<string, int>();
    }

    public void AddItemIntoInventory(string itemID, int amount)
    {
        if(!Inventory.ContainsKey(itemID))
        {
            Inventory[itemID] = 0;
        }
        Inventory[itemID] += amount;
    }

    public bool UseItemInInventory(string itemID, int amount)
    {
        if(!Inventory.ContainsKey(itemID)) return false;
        if(Inventory[itemID] < amount) return false;
        Inventory[itemID] -= amount;
        return true;
    }

    public void AddCurrency(int amount) => Currency += amount;

    public bool SpendCurrency(int amount)
    {
        if(Currency < amount) return false;
        Currency -= amount; return true;
    }

}
