using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IFarmable
{
    public float DurationProgress {get; set;}
    public ProductType Product {get; set;}
    public int AmountProduct {get;set;}
    public int LimitAmountProduct{get;set;}
    public float GrowthTime{get;set;}
    public float TimesToMaturity{get;set;}
    public  int CollectProduct();
    public void Update();
}
