using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Button _btn;
    [SerializeField]
    private Image _itemImg;
    private Item _item;
    [SerializeField]
    private TextMeshProUGUI _amountText;
    private InputController _inputController;
    public void Initialise(InputController inputController)
    {
        _btn = GetComponent<Button>();
        _inputController = inputController;
        
    }
    public void SetupItemSlot(string itemId, int amount)
    {
        _item = DataManager.Instance.GetItem(itemId);
        _itemImg.sprite = DataManager.Instance.GetItemSprite(_item.Id);
        _btn.onClick.AddListener(() => _inputController.SelectItem(_item.Id));
        _amountText.text = $"x{amount}";
    }

    private void OnDisable() {
        ResetSlot();
    }

    private void ResetSlot()
    {
        _itemImg.sprite = null;
        _item = null;
        _amountText.text = $"x{0}";
        _btn.onClick.RemoveAllListeners();
    }
}
