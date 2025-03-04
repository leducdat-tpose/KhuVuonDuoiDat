using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager
{
    public static DataManager Instance {get; private set;}

    private DataManager(){
        Instance = this;
    }

    public static DataManager CreateAndInitialise()
    {
        DataManager dataManager = new DataManager();
        dataManager.Initialise();
        return dataManager;
    }

    private ItemDataManager itemDataManager;
    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
    private void Initialise()
    {
        itemDataManager = new ItemDataManager();
        LoadItems();
    }
    
    private string GetCSVFilePath()
    {
        string path;
        if(Application.isEditor||Debug.isDebugBuild)
            path = Path.Combine(Application.dataPath, "Resources", Constant.csvFileName);
        else path = Path.Combine(Application.streamingAssetsPath, "Data", Constant.csvFileName);
        //In window, Path.Combine use backslashes (\) as path separators
        //While other OS, use forward slashes (/).
        path = path.Replace("/", "\\");
        return path;
    }

    private void LoadItems()
    {
        string filePath = GetCSVFilePath();
        Debug.Log($"filePath: {filePath}");

        itemDataManager.LoadItemsFromCSVFile(filePath);
        LoadItemsSprite();
    }
    
    //Load sprite for item at once
    private void LoadItemsSprite()
    {
        var database = itemDataManager?.GetItemDatabase();
        foreach(KeyValuePair<string, Item> item in database)
        {
            Sprite sprite = Resources.Load<Sprite>($"{Constant.spriteFolderPath}/{item.Value.ItemSpriteName}");
            if(sprite != null) spriteCache[item.Key] = sprite;
            else Debug.LogWarning($"Failed to load sprite for {item.Key}: {item.Value.ItemSpriteName}");
        }
    }

    public bool IsContainItem(string itemId) => itemDataManager.IsContain(itemId);

    //Get item base on item name
    public Item GetItem(string itemId) => itemDataManager.GetItem(itemId);

    //Get item base on item name with type T
    public T GetItem<T>(string itemId) where T: Item
    => itemDataManager.GetItem<T>(itemId);


    public Sprite GetItemSprite(string itemId)
    {
        if(spriteCache.TryGetValue(itemId, out Sprite itemSprite)) return itemSprite;
        return null;
    }

    public Item CreateItem(string itemId) => itemDataManager.CreateItem(itemId);

    public T CreateItem<T>(string itemId) where T: Item
    => itemDataManager.CreateItem<T>(itemId);

    //Checking data in csv is correct or not
    public void DebugDataCSV()
    {
        var database = itemDataManager.GetItemDatabase();
        foreach(KeyValuePair<string, Item> item in database)
        {
            Debug.Log($"Item name:{item.Key}, item:{item.Value}");
            Debug.Log($"Sprite: {GetItemSprite(item.Key)}");
            Debug.Log($"Type: {item.Value.Type}");
        }
    }
}
