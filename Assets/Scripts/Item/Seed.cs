using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Seed : Item, IFarmable
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
        } else Type = ItemType.Seed;
        Id = rowData[1];
        Value = int.Parse(rowData[2]);
        DurationProgress = float.Parse(rowData[3]);
        Product = (ProductType)Enum.Parse(typeof(ProductType), rowData[4]);
        ItemSpriteName = rowData[5];
        GrowthTime = 0; TimesToMaturity = 40;
        AmountProduct = 0; LimitAmountProduct = 2;
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

    public override Item Clone()
    {
        Seed copy = new Seed();
        copy.Type = this.Type;
        copy.Id = this.Id;
        copy.Value = this.Value;
        copy.DurationProgress = this.DurationProgress;
        copy.Product = this.Product;
        copy.ItemSpriteName = this.ItemSpriteName;
        copy.GrowthTime = this.GrowthTime; 
        copy.TimesToMaturity = this.TimesToMaturity;
        copy.AmountProduct = this.AmountProduct;
        copy.LimitAmountProduct = this.LimitAmountProduct;
        return copy;
    }
}