using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlotState
{
    Empty,
    InProgress,
    Finish
}

// public class PlotBehaviour : MonoBehaviour
// {   
//     private FarmPlotData _plotData;
//     [Header("Propertiles")]
//     [SerializeField]
//     private SpriteRenderer _soilRenderer;
//     [SerializeField]
//     private Sprite _emptyPlot;
//     [SerializeField]
//     private Sprite _haveSeedPlot;
//     [SerializeField]
//     private Sprite _collectProductPlot;
    
//     [field:SerializeField]
//     public PlotState CurrentState;

//     public void Initialise()
//     {
//         _soilRenderer = GetComponent<SpriteRenderer>();
//         CurrentState = PlotState.Empty;
//         _plotData = new FarmPlotData();
//         _plotData.InitialiseDebug();
//         _emptyPlot = _soilRenderer.sprite;
//         _haveSeedPlot = Resources.Load<Sprite>(path: Constant.PlantSprite);
//     }
//     private void Start() {
//         Initialise();
//     }
// #region Plant
//     private void ReadyToFarm()
//     {
//         CurrentState = PlotState.Empty;
//         _plotData.ResetPlot();
//         _soilRenderer.sprite = _emptyPlot;
//     }
//     public void Farm(IFarmable item)
//     {
//         if(CurrentState != PlotState.Empty) return;
//         if(!_plotData.Farming(item)) return;
//         CurrentState = PlotState.InProgress;
//         _soilRenderer.sprite = _haveSeedPlot;
//         // StartCoroutine(GrowingProgress());
//     }
// #endregion

// #region Harvest
//     public void ReadyToHarvest()
//     {
//         CurrentState = PlotState.Finish;
//         _soilRenderer.sprite = _collectProductPlot;
//     }
//     public void Harvest()
//     {
//         if(CurrentState != PlotState.Finish) return;
//         _soilRenderer.sprite = _emptyPlot;
//         ReadyToFarm();
//     }
// #endregion
    
    

//     public void SetSpriteCollectProduct(Sprite sprite)
//     {
//         _collectProductPlot = sprite;
//     }
// }

public class Plot
{
    public string Id;
    public float UnlockCost;
    public bool isUnlocked;
    public IFarmable CurrentObject;

    public Plot(string id)
    {
        Id = id;
        UnlockCost = 100;
        isUnlocked = true;
        CurrentObject = null;
    }

    public void Update()
    {
        if(CurrentObject == null) return;
        CurrentObject.Update();
    }
    public void StartFarm(IFarmable item)
    {
        if(!isUnlocked) return;
        CurrentObject = item;
    }
    public void Harvest()
    {
    }
    public void ResetPlot()
    {
        CurrentObject = null;
    }
}