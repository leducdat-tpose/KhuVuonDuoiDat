using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotUIManager : MonoBehaviour
{
    // [SerializeField]
    // private PlotController _plotController;
    [SerializeField]
    private Canvas _plotCanvas;
    [Header("UI Elements")]
    [SerializeField]
    private Button _plantBtn;
    [SerializeField]
    private Button _harvestBtn;

    public void Initialise()
    {
        _plotCanvas = GetComponent<Canvas>();
        // _plotController = GetComponentInParent<PlotController>();
        _plantBtn = transform.Find("PlantBtn").GetComponent<Button>();
        _harvestBtn = transform.Find("HarvestBtn").GetComponent<Button>();
        
    }

    private void Start() {
        Initialise();
        _plotCanvas.worldCamera = Camera.main;
        // _plantBtn.onClick.AddListener(_plotController.FarmSeed);
        // _harvestBtn.onClick.AddListener(_plotController.Harvest);
    }
    private void Update() {
        
    }
}
