using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public enum ItemType{
    Seed = 0,
    Animal = 1,
    Product = 2
}

public abstract class Item
{
    public string Id {get; protected set;}
    public string ItemSpriteName {get; protected set;}
    public ItemType Type {get; protected set;}
    public int Value{get; protected set;}

    public abstract Item Clone();
    public virtual T Clone<T>()
    {
        return (T)(object)Clone();
    }

    public abstract void LoadData(string[] rowData);
}

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