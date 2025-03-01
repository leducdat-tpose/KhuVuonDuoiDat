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
    [field:SerializeField]
    public FarmPlotData PlotData{get;private set;}
    [Header("Propertiles")]
    [SerializeField]
    private SpriteRenderer _soilRenderer;
    [SerializeField]
    private SpriteRenderer _plantRenderer;
    
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
        PlotData = new FarmPlotData();
        PlotData.InitialiseDebug();
    }
    private void Start() {
        Initialise();
    }
#region Plant
    private void ReadyToPlant()
    {
        CurrentState = PlotState.Empty;
        Debug.Log("Ready to plant");
        _soilRenderer.color = Color.white;
    }
    public void Plant()
    {
        if(CurrentState != PlotState.Empty) return;
        Debug.Log(PlotData.CanPlant());
        if(!PlotData.CanPlant()) {
            Debug.Log("Don't has seed or not buy yet!");
            return;
        }
        Debug.Log("Plant seed");
        _soilRenderer.color = Color.grey;
        StartCoroutine(GrowingProgress());
    }
#endregion

#region Harvest
    public void ReadyToHarvest()
    {
        CurrentState = PlotState.Finish;
        Debug.Log("Ready to harvest");
        _soilRenderer.color = Color.green;
    }
    public void Harvest()
    {
        if(CurrentState != PlotState.Finish) return;
        Debug.Log("Harvest");
        ReadyToPlant();
    }
#endregion
    
    
    private IEnumerator GrowingProgress()
    {
        yield return new WaitForSecondsRealtime(PlotData.CurrentSeed.DurationGrowth);
        ReadyToHarvest();
    }
}