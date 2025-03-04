using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
public enum ProductType{
    None,
    Tomato,
    Blueberry,
    Milk,
}

public class Product : Item
{
    public float DurationProgress {get; set;}
    public ProductType productType {get; set;}

    public override Item Clone()
    {
        Product copy = new Product();
        copy.Type = this.Type;
        copy.Id = this.Id;
        copy.Value = this.Value;
        copy.DurationProgress = this.DurationProgress;
        copy.productType = this.productType;
        copy.ItemSpriteName = this.ItemSpriteName;
        return copy;
    }

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