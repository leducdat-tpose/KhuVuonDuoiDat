using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlotController : MonoBehaviour
{
    [SerializeField]
    private PlotBehaviour _behaviour;
    [SerializeField]
    private Canvas _interactionCanvas;
    
    public void Initialise()
    {
        _behaviour = GetComponent<PlotBehaviour>();
        _interactionCanvas = GetComponentInChildren<Canvas>(includeInactive:true);
    }
    private void Start() {
        Initialise();
        SetActiveUI(false);
    }

    private void OnMouseDown() {
        Debug.Log("mouse down");
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;
        SetActiveUI(true);
    }

    private void OnMouseExit() {
        if(!_interactionCanvas.gameObject.activeSelf) return;
        Debug.Log("mouse exit");
        SetActiveUI(false);
    }

    public void PlantSeed()
    {
        _behaviour.Plant();
        SetActiveUI(false);
    }
    public void Harvest()
    {
        _behaviour.Harvest();
        SetActiveUI(false);
    }

    public void SetActiveUI(bool option) => _interactionCanvas.gameObject.SetActive(option);

}
