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
        if(_interactionCanvas.gameObject.activeSelf) return;
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;
        SetActiveUI(true);
    }

    private void OnMouseExit() {
        if(!_interactionCanvas.gameObject.activeSelf) return;
        SetActiveUI(false);
    }

    public void FarmSeed()
    {
        var animal = DataManager.Instance.GetItem<Animal>("Cow");
        _behaviour.Farm(animal);
        _behaviour.SetSpriteCollectProduct(DataManager.Instance.GetItemSprite(animal.Name));
        SetActiveUI(false);
    }
    public void Harvest()
    {
        _behaviour.Harvest();
        SetActiveUI(false);
    }

    public void SetActiveUI(bool option) => _interactionCanvas.gameObject.SetActive(option);

}
