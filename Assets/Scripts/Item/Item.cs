using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Newtonsoft.Json;
[Serializable]
public enum ItemType{
    Seed = 0,
    Animal = 1,
    Product = 2
}
[Serializable]
public abstract class Item
{
    [JsonProperty]
    public string Id {get; protected set;}
    [JsonProperty]
    public string ItemSpriteName {get; protected set;}
    [JsonProperty]
    public ItemType Type {get; protected set;}
    [JsonProperty]
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