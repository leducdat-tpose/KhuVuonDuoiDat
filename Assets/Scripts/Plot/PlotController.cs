using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlotController : MonoBehaviour
{
    public string Id{get;private set;}
    [SerializeField]
    private SpriteRenderer _plotRender;
    [SerializeField]
    private Sprite _spriteEmpty;
    [SerializeField]
    private Sprite _spriteGrowingObj;
    [SerializeField]
    private Sprite _spriteHaveProduct;
    [SerializeField]
    private Sprite _spriteLocked;
    private InputController _inputcontroller;
    public void InitialOwnedPlot(string Id, GameController gameController, InputController inputController)
    {
        Initialise(Id);
        _inputcontroller = inputController;
        gameController.OnPlotUpdated += OnPlotUpdated;
    }

    public void InitialLockedPlot(GameController gameController, InputController inputController)
    {
        _plotRender = GetComponent<SpriteRenderer>();
        _plotRender.color = Color.grey;
        _plotRender.sprite = _spriteLocked;
        _inputcontroller = inputController;
        gameController.OnPlotUpdated += OnPlotUpdated;
    }

    public void OnPlotUpdated(Plot plot)
    {
        if(plot.Id != Id) return;
        UpdateVisual(plot);
    }

    public void Initialise(string Id)
    {
        this.Id = Id;
        _plotRender = GetComponent<SpriteRenderer>();
        ResetPlotController();
    }
    private void ResetPlotController()
    {
        _plotRender.sprite = _spriteEmpty;
        _spriteHaveProduct = null;
    }
    private void UpdateVisual(Plot plot)
    {
        if(!plot.isUnlocked)
        {
            _plotRender.sprite = _spriteLocked;
            return;
        }
        if(plot.CurrentObject != null)
        {
            SetSpriteProduct(plot.CurrentObject);
            if(plot.HaveProduct()) _plotRender.sprite = _spriteHaveProduct;
            else _plotRender.sprite = _spriteGrowingObj;
        }
        else _plotRender.sprite = _spriteEmpty;
    }
    private void OnMouseDown() {
        Debug.Log("OnMouseDown on PlotController");
        _inputcontroller.SelectTool(InputController.ToolType.Plant);
        _inputcontroller.SelectPlot(Id);
    }

    private void SetSpriteProduct(IFarmable farmableItem)
    {
        if(_spriteHaveProduct != null) return;
        Item item = (Item)farmableItem;
        _spriteHaveProduct = DataManager.Instance?.GetItemSprite(item.Id);
    }

}
