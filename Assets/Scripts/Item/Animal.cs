using System.Collections;
using System.Collections.Generic;
using System;

public class Animal : Item, IFarmable
{
    public float DurationProgress {get; set;}
    public ProductType Product {get; set;}
    public int AmountProduct {get;set;}
    public int LimitAmountProduct{get;set;}
    public float GrowthTime{get;set;}
    public float TimesToMaturity{get;set;}
    public override void LoadData(string[] rowData)
    {
        //Order: Type, Name, Value, DurationProgress, Product, SpriteName

        if(Enum.TryParse(rowData[0], out ItemType type))
        {
            Type = type;
        } else Type = ItemType.Animal;
        Id = rowData[1];
        Value = int.Parse(rowData[2]);
        DurationProgress = float.Parse(rowData[3]);
        Product = (ProductType)Enum.Parse(typeof(ProductType), rowData[4]);
        ItemSpriteName = rowData[5];
    }

    public void Update()
    {
        if(AmountProduct >= LimitAmountProduct) return;
        GrowthTime++;
        if(GrowthTime < TimesToMaturity) return;
        GrowthTime = 0;
        AmountProduct++;
    }
    public int CollectProduct()
    {
        var amount = AmountProduct;
        LimitAmountProduct -= AmountProduct;
        AmountProduct = 0;
        return amount;
    }
}