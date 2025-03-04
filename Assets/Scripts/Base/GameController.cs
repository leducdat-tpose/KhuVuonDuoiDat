using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class GameController
{
    private Farm _farm;

    public event Action<Plot> OnPlotUpdated;
    public event Action<PlayerData> OnPlayerDataChanged;
    public GameController(int initCurrency, int initPlotCount)
    {
        _farm = new Farm(initCurrency, initPlotCount);
    }
    
    public void Update()
    {
        _farm.Update();
    }

    public List<Plot> GetAllPlots() => _farm.Plots;

    private Plot FindPlot(string plotId) 
        => _farm.Plots.FirstOrDefault(plot => plot.Id == plotId);

    public bool UsePlot<T>(string plotId, string itemId) where T: Item
    {
        Plot plot = FindPlot(plotId);
        if(plot == null) return false;
        if(_farm.PlayerData.Inventory[itemId] <= 0) return false;
        T item = DataManager.Instance.CreateItem<T>(itemId);
        if(item == null) return false;
        bool result = plot.StartFarm(item);
        if(result)
        {
            _farm.PlayerData.RemoveItemInInventory(item.Id, 1);
            OnPlotUpdated?.Invoke(plot);
            OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        }
        return result;
    }

    public bool BuyItem(string itemId, int amount)
    {
        bool result = _farm.BuyItem(itemId, amount);
        if(result)
        {
            OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        }
        return result;
    }

    public bool SellItem(string itemId, int amount)
    {
        bool result = _farm.SellItem(itemId, amount);
        if(result)
        {
            OnPlayerDataChanged?.Invoke(_farm.PlayerData);
        }
        return result;
    }

    public PlayerData GetPlayerData() => _farm.PlayerData;

}   



// public static class SaveLoadJSON
// {
//     public static void SaveData(PlayerData playerData)
//     {
//         string savePlayerData = JsonUtility.ToJson(playerData);
        
//         Debug.Log(savePlayerData);
//         File.WriteAllText(Constant.SaveFilePath, savePlayerData);
//     }
//     public static PlayerData LoadData()
//     {
//         if(File.Exists(Constant.SaveFilePath))
//         {
//             string loadPlayerData = File.ReadAllText(Constant.SaveFilePath);
//             Debug.Log(loadPlayerData);
//             return JsonUtility.FromJson<PlayerData>(loadPlayerData);
//         }
//         Debug.Log($"Not exist or can't load data from file: {Constant.SaveFilePath}");
//         return null;
//     }
//     public static void DeleteSaveFile()
//     {
//         if(!File.Exists(Constant.SaveFilePath)) return;
//         Debug.Log("Delete save file success");
//         File.Delete(Constant.SaveFilePath);
//     }
// }

// #if UNITY_EDITOR
// [CustomEditor(typeof(GameController))]
// public class GameControllerEditor: Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();
//         GameController controller = (GameController)target;
//         if(GUILayout.Button("Save PlayerData"))
//         {
//             SaveLoadJSON.SaveData(controller.PlayerData);
//         }
//         if(GUILayout.Button("Load PlayerData"))
//         {
//             controller.SetPlayerData(SaveLoadJSON.LoadData());
//         }
//     }
// }
// #endif