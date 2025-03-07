using System;
using System.Collections;
using System.Collections.Generic;

public class Worker
{
    public enum WorkingState{
        Idle,
        Working,
        Plant,
        Harvest
    }
    private double DurationWorking;
    private DateTime _timeAction;
    private GameController _gameController;
    public Plot CurrentPlot{get; private set;}
    public FarmableItem CurrentItem{get; private set;}
    private string itemId = "";
    public WorkingState CurrentState{get; private set;}

    private Worker(GameController gameController)
    {
        _gameController = gameController;
    }

    public static Worker CreateAndInit(GameController gameController)
    {
        Worker worker = new Worker(gameController);
        worker.Initialise();
        return worker;
    }

    private void Initialise()
    {
        DurationWorking = Constant.DurationWorkingWorker;
        CurrentState = WorkingState.Idle;
    }

    public void Update()
    {
        if(CurrentState == WorkingState.Idle) return;
        OnWorking();
    }
    public void StartWorking(Plot plot)
    {
        CurrentPlot = plot;
        itemId = plot.CurrentItem.Id;
        CurrentState = WorkingState.Working;
    }
    private void OnWorking()
    {
        CheckingAction();
        if(CurrentState == WorkingState.Working) return;
        TimeSpan span = DateTime.Now - _timeAction;
        if(span.TotalSeconds > Constant.DurationWorkingWorker)
        {
            if(CurrentState == WorkingState.Harvest)
            {
                if(CurrentPlot.CurrentItem != null)
                {
                    _gameController.HarvestItem(plotId: CurrentPlot.Id);
                    CurrentState = WorkingState.Working;
                }
            }
            else if(CurrentState == WorkingState.Plant)
            {
                bool result = _gameController.PlantItem(plotId:CurrentPlot.Id, itemId);
                if(result) CurrentState = WorkingState.Working;
                else StopWorking();
            }
        }
    }

    private void CheckingAction()
    {
        if(CurrentState != WorkingState.Working) return;
        if(CurrentPlot.CurrentItem != null)
        {
            if(CurrentPlot.CurrentItem.AmountProduct != 0)
            {
                CurrentState = WorkingState.Harvest;
                _timeAction = DateTime.Now;
            }
        }
        else
        {
            CurrentState = WorkingState.Plant;
            _timeAction = DateTime.Now;
        }
    }
    public void StopWorking()
    {
        CurrentState = WorkingState.Idle;
        CurrentPlot = null;
        CurrentItem = null;
        itemId = "";
    }
}
