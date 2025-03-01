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
    }
    private void Update() {
        
    }

    public void SetPlayerData(PlayerData data)
    {
        if(data == null) return;
        this.PlayerData = data;
    }
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