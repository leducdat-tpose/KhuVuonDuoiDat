using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField]
    private Button _closeBtn;
    [SerializeField]
    private Button _buyBtn;
    private GameController _gameController;
    private InputController _inputController;
    [SerializeField]
    private GameObject _gridPanel;
    private List<ItemSlot> _itemSlots = new List<ItemSlot>();
    public void Initialise(GameController gameController, InputController inputController)
    {
        _gameController = gameController;
        _inputController = inputController;
        _closeBtn.onClick.AddListener(() => {
            this.transform.gameObject.SetActive(false);
            _inputController.ResetValue();
        });
        _buyBtn.onClick.AddListener(() =>{
            _inputController.SelectCommand(InputController.Command.Buy);
        });
        foreach(Transform transform in _gridPanel.transform)
        {
            if(transform.TryGetComponent<ItemSlot>(out ItemSlot slot))
            {
                _itemSlots.Add(slot);
                slot.Initialise(_inputController);
                slot.gameObject.SetActive(false);
            }
        }
    }
    private void SetupItemSlot()
    {
        int i = 0;
        List<FarmableItem> listItem = DataManager.Instance.GetItems<FarmableItem>();
        foreach(FarmableItem item in listItem)
        {
            _itemSlots[i].gameObject.SetActive(true);
            _itemSlots[i].SetupItemSlot(item.Id, item.Value);
            i++;
        }
    }
    private void ResetItemSlot()
    {
        foreach(ItemSlot slot in _itemSlots)
        {
            if(!slot.gameObject.activeSelf) break;
            slot.gameObject.SetActive(false);
        }
    }
    private void OnEnable() {
        SetupItemSlot();
        _inputController.SelectCommand(InputController.Command.Buy);
    }
    private void OnDisable() {
        ResetItemSlot();
    }
}
