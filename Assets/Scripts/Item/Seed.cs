using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class Seed : Item, IFarmable
{
    public double GrowthDuration {get; set;}
    public ProductType Product {get; set;}
    public int AmountProduct {get;set;}
    public int LimitAmountProduct{get;set;}
    public DateTime PlantedTime {get;set;}
    public override void LoadData(string[] rowData)
    {
        //Order: Type, Name, Value, DurationProgress, Product, SpriteName

        if(Enum.TryParse(rowData[0], out ItemType type))
        {
            Type = type;
        } else Type = ItemType.Seed;
        Id = rowData[1];
        Value = int.Parse(rowData[2]);
        GrowthDuration = float.Parse(rowData[3]);
        Product = (ProductType)Enum.Parse(typeof(ProductType), rowData[4]);
        ItemSpriteName = rowData[5];
        AmountProduct = 0; 
        LimitAmountProduct = 2;
    }

    public void StartGrowing()
    {
        PlantedTime = DateTime.Now;
    }

    public void Update()
    {
        if(AmountProduct == LimitAmountProduct) return;
        TimeSpan span = DateTime.Now - PlantedTime;
        if(span.TotalSeconds < GrowthDuration) return;
        AmountProduct += 1;
        GrowthDuration += span.TotalSeconds;
    }
    public int CollectProduct()
    {
        var amount = AmountProduct;
        LimitAmountProduct -= AmountProduct;
        AmountProduct = 0;
        return amount;
    }

    public bool IsOutOfProduct() => LimitAmountProduct == 0;
    public int GetTimeCountGrowth()
    {
        if(AmountProduct == LimitAmountProduct) return 0;
        TimeSpan span = DateTime.Now - PlantedTime;
        return (int)(GrowthDuration - span.TotalSeconds);
    }
}