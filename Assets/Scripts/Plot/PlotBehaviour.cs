using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlotState
{
    Empty,
    Growing,
    Finish
}

public class PlotBehaviour : MonoBehaviour
{   
    private FarmPlotData _plotData;
    private FarmableFactory _farmableFactory;
    [Header("Propertiles")]
    [SerializeField]
    private SpriteRenderer _soilRenderer;
    [SerializeField]
    private Sprite _emptyPlot;
    [SerializeField]
    private Sprite _haveSeedPlot;
    
    [field:SerializeField]
    public PlotState CurrentState;

    public void Initialise()
    {
        _soilRenderer = GetComponent<SpriteRenderer>();
        CurrentState = PlotState.Empty;
        _plotData = new FarmPlotData();
        _plotData.InitialiseDebug();
        _farmableFactory = new BlueberryFactory();
        _emptyPlot = _soilRenderer.sprite;
        _haveSeedPlot = _soilRenderer.sprite;
    }
    private void Start() {
        Initialise();
    }
#region Plant
    private void ReadyToPlant()
    {
        CurrentState = PlotState.Empty;
        _soilRenderer.color = Color.white;
        _soilRenderer.sprite = _emptyPlot;
    }
    public void Plant()
    {
        if(CurrentState != PlotState.Empty) return;
        _plotData.Plant(_farmableFactory.CreateFarmableItem());
        if(!_plotData.CanPlant()) return;
        _soilRenderer.color = Color.grey;
        _soilRenderer.sprite = _haveSeedPlot;
        StartCoroutine(GrowingProgress());
    }
#endregion

#region Harvest
    public void ReadyToHarvest()
    {
        CurrentState = PlotState.Finish;
        _soilRenderer.color = Color.white;
        _soilRenderer.sprite = Resources.Load<Sprite>(path:_plotData.CurrentSeed.Sprite);
    }
    public void Harvest()
    {
        if(CurrentState != PlotState.Finish) return;
        _soilRenderer.sprite = _emptyPlot;
        ReadyToPlant();
    }
#endregion
    
    
    private IEnumerator GrowingProgress()
    {
        yield return new WaitForSecondsRealtime(_plotData.CurrentSeed.DurationGrowth);
        ReadyToHarvest();
    }
}