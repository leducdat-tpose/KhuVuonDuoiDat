using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeedType
{
    Tomato,
    Blueberry,
    Strawberry
}
[System.Serializable]
public class Seed : IFarmable
{
    public string Name{get; set;}
    public Product ProductPrefab {get; set;}

    public float DurationGrowth {get; set;}
    public SeedType Type{get; set;}

    public Seed(){
        this.Name = "TomatoSeed";
        this.DurationGrowth = 3f;
        this.Type = SeedType.Tomato;
        ProductPrefab = new Product{
            Name = "Tomato",
            SellMoney = 100,
        };
    }
}
