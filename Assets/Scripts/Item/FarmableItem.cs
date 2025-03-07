using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class FarmableItem : Item, IFarmable
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
        var toolLevel = DataManager.Instance.PlayerData.ToolLevel;
        GrowthDuration *= (double)(1 - ((toolLevel - 1) * Constant.ToolEfficient));
        PlantedTime = DateTime.Now;
    }
    public int CollectProduct()
    {
        var amount = AmountProduct;
        LimitAmountProduct -= AmountProduct;
        AmountProduct = 0;
        return amount;
    }
    public bool IsOutOfProduct() => LimitAmountProduct == 0;
    public void Update()
    {
        if(AmountProduct >= LimitAmountProduct) return;
        TimeSpan span = DateTime.Now - PlantedTime;
        if(span.TotalSeconds < GrowthDuration) return;
        AmountProduct += (int)(span.TotalSeconds/GrowthDuration);
        if(AmountProduct > LimitAmountProduct) AmountProduct = LimitAmountProduct;
        GrowthDuration += span.TotalSeconds;
    }
    public int GetTimeCountGrowth()
    {
        if(AmountProduct >= LimitAmountProduct) return 0;
        TimeSpan span = DateTime.Now - PlantedTime;
        if((int)(span.TotalSeconds/GrowthDuration) >= LimitAmountProduct) return 0;
        return (int)(GrowthDuration - span.TotalSeconds);
    }

}