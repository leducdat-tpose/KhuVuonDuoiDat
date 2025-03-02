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
        _emptyPlot = _soilRenderer.sprite;
        _haveSeedPlot = Resources.Load<Sprite>(path: Constant.PlantSprite);
    }
    private void Start() {
        Initialise();
    }
#region Plant
    private void ReadyToFarm()
    {
        CurrentState = PlotState.Empty;
        _plotData.ResetPlot();
        _soilRenderer.sprite = _emptyPlot;
    }
    public void Farm(Item item)
    {
        if(CurrentState != PlotState.Empty) return;
        if(!_plotData.Farming(item)) return;
        _soilRenderer.sprite = _haveSeedPlot;
        StartCoroutine(GrowingProgress());
    }
#endregion

#region Harvest
    public void ReadyToHarvest()
    {
        CurrentState = PlotState.Finish;
        _soilRenderer.sprite = DataManager.Instance.GetItemSprite(_plotData.CurrentItem.Name);
    }
    public void Harvest()
    {
        if(CurrentState != PlotState.Finish) return;
        _soilRenderer.sprite = _emptyPlot;
        ReadyToFarm();
    }
#endregion
    
    
    private IEnumerator GrowingProgress()
    {
        yield return new WaitForSecondsRealtime(3f);
        ReadyToHarvest();
    }
}