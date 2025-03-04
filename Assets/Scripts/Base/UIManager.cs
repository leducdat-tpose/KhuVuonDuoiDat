using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameController _gameController;
    private InputController _inputController;

    [SerializeField]
    private Button _buyItemBtn;
    [SerializeField]
    private Button _sellItemBtn;
    [SerializeField]
    private Button _debugBtn;

    public void Initialise(GameController gameController, InputController inputController)
    {
        _gameController = gameController;
        _inputController = inputController;
        // _buyItemBtn.onClick.AddListener(TestFunc1);
        // _sellItemBtn.onClick.AddListener(TestFunc2);
    }
    public void UpdateAll()
    {

    }

    private void TestFunc1()
    {
        foreach(Plot plot in _gameController.GetAllPlots())
        {
            Debug.Log($"Plot:{plot.Id}");
        }
        Item item1 = DataManager.Instance.CreateItem("TomatoSeed");
        Item item2 = DataManager.Instance.CreateItem<Item>("BlueberrySeed");
        Item item3 = DataManager.Instance.CreateItem<Animal>("Cow");
        Debug.Log(item1);
        Debug.Log(item2);
        Debug.Log(item3);
    }

    private void TestFunc2()
    {
        Item item1 = DataManager.Instance.GetItem("TomatoSeed");
        Item item2 = DataManager.Instance.GetItem<Item>("BlueberrySeed");
        Item item3 = DataManager.Instance.GetItem<Animal>("Cow");
        Debug.Log(item1);
        Debug.Log(item2);
        Debug.Log(item3);
    }
    private void OnGUI() {
        if(GUI.Button(new Rect(10, 10, 70, 30), "Test 1"))
        {
            TestFunc1();
        }
        if(GUI.Button(new Rect(10, 40, 70, 30), "Test 2"))
        {
            TestFunc2();
        }
    }
}
