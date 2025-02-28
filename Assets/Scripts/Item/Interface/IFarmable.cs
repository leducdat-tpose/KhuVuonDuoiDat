using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFarmable
{
    public float DurationGrowth {get; set;}
    public Product ProductPrefab {get; set;}
}
