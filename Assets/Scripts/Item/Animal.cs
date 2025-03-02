using System.Collections;
using System.Collections.Generic;
using System;

public class Animal : Item, IFarmable
{
    public float DurationProgress {get; set;}
    public string Product {get; set;}
    public override void LoadData(string[] rowData)
    {
        //Order: Type, Name, Value, DurationProgress, Product, SpriteName

        if(Enum.TryParse(rowData[0], out ItemType type))
        {
            Type = type;
        } else Type = ItemType.Animal;
        Name = rowData[1];
        Value = int.Parse(rowData[2]);
        DurationProgress = float.Parse(rowData[3]);
        Product = rowData[4];
        ItemSpriteName = rowData[5];
    }
}