using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public enum ItemType{
    Seed = 0,
    Animal = 1,
}

public abstract class Item
{
    public string Name {get; protected set;}
    public string ItemSpriteName {get; protected set;}
    public ItemType Type {get; protected set;}

    public abstract void LoadData(string[] rowData);
}