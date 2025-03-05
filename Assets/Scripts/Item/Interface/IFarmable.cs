using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IFarmable
{
    public double GrowthDuration {get; set;}
    public ProductType Product {get; set;}
    public int AmountProduct {get;set;}
    public int LimitAmountProduct{get;set;}
    public DateTime PlantedTime{get;set;}
    public void StartGrowing();
    public  int CollectProduct();
    public bool IsOutOfProduct();
    public void Update();
    public int GetTimeCountGrowth();
}
