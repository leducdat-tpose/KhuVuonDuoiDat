using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
[Serializable]
public enum ProductType{
    None,
    Tomato,
    Blueberry,
    Strawberry,
    Milk,
}
[Serializable]
public class Product : Item
{
    public float DurationProgress {get; set;}
    public ProductType productType {get; set;}
    public override void LoadData(string[] rowData)
    {
        if(Enum.TryParse(rowData[0], out ItemType type))
        {
            Type = type;
        } else Type = ItemType.Product;
        Id = rowData[1];
        Value = int.Parse(rowData[2]);
        DurationProgress = float.Parse(rowData[3]);
        productType = (ProductType)Enum.Parse(typeof(ProductType), rowData[4]);
        ItemSpriteName = rowData[5];
    }
}