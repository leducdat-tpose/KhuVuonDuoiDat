using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int Money;
    public Dictionary<string, object> Inventory;
    public PlayerData()
    {
        Money = 0;
        Inventory = new Dictionary<string, object>();
    }
}
