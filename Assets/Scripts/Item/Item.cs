using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
[Serializable]
public enum ItemType{
    Seed = 0,
    Animal = 1,
    Product = 2
}
[Serializable]
public abstract class Item
{
    public string Id {get; protected set;}
    public string ItemSpriteName {get; protected set;}
    public ItemType Type {get; protected set;}
    public int Value{get; protected set;}
    public abstract void LoadData(string[] rowData);
}
[Serializable]
public class ItemData
{
    public string Id{get; private set;}
    public string[] Data{get; private set;}
    public ItemData(string Id, string[] Data)
    {
        this.Id = Id;
        this.Data = Data;
    }
}