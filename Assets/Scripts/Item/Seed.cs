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

    public string Sprite{get; set;}
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

    public virtual void Grow()
    {
        
    }
}


public class Tomato : Seed
{
    public override void Grow()
    {
        Debug.Log("Growing Tomato");
    }
    public Tomato()
    {
        Type = SeedType.Tomato;
        Name = "TomatoSeed";
        DurationGrowth = 4f;
        ProductPrefab = new Product{
            Name = "Tomato fruit",
            SellMoney = 100,
        };
        Sprite = Constant.TomatoSprite;
    }
}
public class Blueberry : Seed
{
    public Blueberry()
    {
        Type = SeedType.Blueberry;
        Name = "BlueberrySeed";
        DurationGrowth = 10f;
        ProductPrefab = new Product{
            Name = "Blueberry fruit",
            SellMoney = 100,
        };
        Sprite = Constant.BlueberrySprite;
    }
    public override void Grow()
    {
        Debug.Log("Growing Blueberry");
    }
}