using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Item
{
    public int DurationGrowth {get; private set;}
    public string Product {get; private set;}
    public override void LoadData(string[] rowData)
    {
        //Order: Name, DurationGrowth, Product, Sprite, Type
        Name = rowData[0];
        DurationGrowth = int.Parse(rowData[1]);
        Product = rowData[2];
        ItemSpriteName = rowData[3];
        if(Enum.TryParse(rowData[4], out ItemType type))
        {
            Type = type;
        } else Type = ItemType.Seed;
    }
}