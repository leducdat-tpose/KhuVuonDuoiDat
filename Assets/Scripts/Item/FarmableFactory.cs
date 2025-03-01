using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FarmableFactory
{
    public abstract Seed CreateFarmableItem();
}

public class TomatoFactory : FarmableFactory
{
    public override Seed CreateFarmableItem()
    {
        return new Tomato();
    }
}

public class BlueberryFactory : FarmableFactory
{
    public override Seed CreateFarmableItem()
    {
        return new Blueberry();
    }
}
