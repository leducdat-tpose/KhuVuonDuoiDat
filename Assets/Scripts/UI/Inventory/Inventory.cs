using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Button _closeBtn;
    private GameController _gameController;
    private PlayerData _playerData;
    private InputController _inputController;
    [SerializeField]
    private ItemSlot[] _itemSlots;
    public void Initialise(GameController gameController, InputController inputController)
    {
        _gameController = gameController;
        _inputController = inputController;
        _closeBtn.onClick.AddListener(() => this.transform.gameObject.SetActive(false));
        foreach(ItemSlot slot in _itemSlots)
        {
            slot.Initialise(_inputController);
            slot.gameObject.SetActive(false);
        }
        _playerData = _gameController.GetPlayerData();
    }
    private void OnEnable() {
        int i = 0;
        foreach(KeyValuePair<string, int> item in _playerData.Inventory)
        {
            _itemSlots[i].gameObject.SetActive(true);
            _itemSlots[i].SetupItemSlot(item.Key, item.Value);
            i++;
        }
    }
    private void OnDisable() {
        foreach(ItemSlot slot in _itemSlots)
        {
            if(!slot.gameObject.activeSelf) break;
            slot.gameObject.SetActive(false);
        }
    }
}
