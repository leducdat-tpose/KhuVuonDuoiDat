using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEditor;

public class GameController : MonoBehaviour
{
    public static GameController Instance{get; private set;}
    public PlayerData PlayerData{get; private set;}
    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }
    private void Start() {
        PlayerData = new PlayerData();
        PlayerData.AddCurrency(100);
        PlayerData.AddItemIntoInventory(nameof(TomatoSeed), 1);
        PlayerData.AddItemIntoInventory(nameof(BlueberrySeed), 2);
    }
    private void Update() {
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            CheckingPlayerData();
        }
    }

    public void SetPlayerData(PlayerData data)
    {
        if(data == null) return;
        this.PlayerData = data;
    }
#if UNITY_EDITOR
    public void CheckingPlayerData()
    {
        if(PlayerData == null) return;
        Debug.Log($"Player curreny:{PlayerData.Currency}");
        var invenData = PlayerData.Inventory;
        foreach(KeyValuePair<string, int> item in invenData)
            Debug.Log($"Item: {item.Key}, amount: {item.Value}");
    }
#endif
}

public static class SaveLoadJSON
{
    public static void SaveData(PlayerData playerData)
    {
        string savePlayerData = JsonUtility.ToJson(playerData);
        
        Debug.Log(savePlayerData);
        File.WriteAllText(Constant.SaveFilePath, savePlayerData);
    }
    public static PlayerData LoadData()
    {
        if(File.Exists(Constant.SaveFilePath))
        {
            string loadPlayerData = File.ReadAllText(Constant.SaveFilePath);
            Debug.Log(loadPlayerData);
            return JsonUtility.FromJson<PlayerData>(loadPlayerData);
        }
        Debug.Log($"Not exist or can't load data from file: {Constant.SaveFilePath}");
        return null;
    }
    public static void DeleteSaveFile()
    {
        if(!File.Exists(Constant.SaveFilePath)) return;
        Debug.Log("Delete save file success");
        File.Delete(Constant.SaveFilePath);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameController))]
public class GameControllerEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameController controller = (GameController)target;
        if(GUILayout.Button("Save PlayerData"))
        {
            SaveLoadJSON.SaveData(controller.PlayerData);
        }
        if(GUILayout.Button("Load PlayerData"))
        {
            controller.SetPlayerData(SaveLoadJSON.LoadData());
        }
    }
}
#endif