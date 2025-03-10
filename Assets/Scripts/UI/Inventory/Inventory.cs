using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Button _closeBtn;
    [SerializeField]
    private Button _useBtn;
    [SerializeField]
    private Button _sellBtn;
    private GameController _gameController;
    private PlayerData _playerData;
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
            _inputController.SelectItem(null);
        });
        _useBtn.onClick.AddListener(() => this.transform.gameObject.SetActive(false));
        _sellBtn.onClick.AddListener(() =>{
            _inputController.SelectCommand(InputController.Command.Sell);
            ResetItemSlot();
            SetupItemSlot();
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
        _playerData = _gameController.GetPlayerData();
    }

    private void SetupItemSlot()
    {
        int i = 0;
        foreach(KeyValuePair<string, int> item in _playerData.Inventory)
        {
            _itemSlots[i].gameObject.SetActive(true);
            _itemSlots[i].SetupItemSlot(item.Key, item.Value);
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
    }
    private void OnDisable() {
        ResetItemSlot();
    }

}
