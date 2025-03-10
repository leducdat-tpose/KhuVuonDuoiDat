using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class DataManager
{
    public static DataManager Instance {get; private set;}

    public PlayerData PlayerData{get; private set;}

    private DataManager(){
        Instance = this;
    }

    public static DataManager CreateAndInitialise()
    {
        DataManager dataManager = new DataManager();
        dataManager.Initialise();
        return dataManager;
    }

    private ItemDataManager _itemDataManager;
    private PlayerDataManager _playerDataManager;
    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
    private void Initialise()
    {
        _itemDataManager = new ItemDataManager();
        _playerDataManager = new PlayerDataManager();
        LoadItems();
    }
    
    private string GetCSVFilePath()
    {
        string path;
        if(Application.isEditor||Debug.isDebugBuild)
            path = Path.Combine(Application.dataPath, "Resources", Constant.FileItemConfig);
        else path = Path.Combine(Application.streamingAssetsPath, "Data", Constant.FileItemConfig);
        //In window, Path.Combine use backslashes (\) as path separators
        //While other OS, use forward slashes (/).
        path = path.Replace("/", "\\");
        return path;
    }

    private string GetJsonFilePath()
    {
        string direct;
        if(Application.isEditor||Debug.isDebugBuild)
            direct = Path.Combine(Application.dataPath, "GameData");
        else direct = Application.persistentDataPath;
        if(!Directory.Exists(direct))
        {
            Directory.CreateDirectory(direct);
        }
        string path = Path.Combine(direct, Constant.PlayerDataJsonFileName);
        path = path.Replace("/", "\\");
        return path;
    }

    private void LoadItems()
    {
        string filePath = GetCSVFilePath();
        Debug.Log($"filePath: {filePath}");

        _itemDataManager.LoadItemsFromCSVFile(filePath);
        LoadItemsSprite();
    }
    public PlayerData LoadPlayerData()
    {
        string path = GetJsonFilePath();
        PlayerData = _playerDataManager.Load(path);
        return PlayerData;
    }

    public void SavePlayerData(PlayerData playerData)
    {
        string path = GetJsonFilePath();
        _playerDataManager.Save(playerData, path);
    }

    public string TestSavePlayerData(PlayerData playerData)
    {
        string path = GetJsonFilePath();
        return _playerDataManager.SaveTest(playerData, path);
    }
    
    //Load sprite for item at once
    private void LoadItemsSprite()
    {
        var database = _itemDataManager?.GetItemDatabase();
        foreach(KeyValuePair<string, Item> item in database)
        {
            Sprite sprite = Resources.Load<Sprite>($"{Constant.spriteFolderPath}/{item.Value.ItemSpriteName}");
            if(sprite != null) spriteCache[item.Key] = sprite;
            else Debug.LogWarning($"Failed to load sprite for {item.Key}: {item.Value.ItemSpriteName}");
        }
    }

    public bool IsContainItem(string itemId) => _itemDataManager.IsContain(itemId);

    //Get item base on item name
    public Item GetItem(string itemId) => _itemDataManager.GetItem(itemId);

    //Get item base on item name with type T
    public T GetItem<T>(string itemId) where T: Item
    => _itemDataManager.GetItem<T>(itemId);

    public List<T> GetItems<T>() where T: Item
    => _itemDataManager.GetItems<T>();

    public Sprite GetItemSprite(string itemId)
    {
        if(spriteCache.TryGetValue(itemId, out Sprite itemSprite)) return itemSprite;
        return null;
    }

    public Item CreateItem(string itemId) => _itemDataManager.CreateItem(itemId);

    public T CreateItem<T>(string itemId) where T: Item
    => _itemDataManager.CreateItem<T>(itemId);

    //Checking data in csv is correct or not
    public void DebugDataCSV()
    {
        var database = _itemDataManager.GetItemDatabase();
        foreach(KeyValuePair<string, Item> item in database)
        {
            Debug.Log(item.GetType());
            Debug.Log($"Item name:{item.Key}, item:{item.Value}");
            Debug.Log($"Sprite: {GetItemSprite(item.Key)}");
            Debug.Log($"Type: {item.Value.Type}");
        }
    }
    public void SetPlayerData(PlayerData playerData)
    => PlayerData = playerData;
}