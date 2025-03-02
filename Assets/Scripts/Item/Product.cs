using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct Product
{
    public string Name{get; set;}
    public int SellMoney{get; set;}
    public string Sprite{get; set;}
    public Product GetProduct(string name, int sellMoney, string sprite){
        return new Product(){
            Name = name,
            SellMoney = sellMoney,
            Sprite = sprite,
        };
    }
}