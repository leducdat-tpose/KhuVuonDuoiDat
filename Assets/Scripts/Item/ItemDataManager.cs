using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ItemDataManager
{
    protected Dictionary<string, Item> itemDatabase = new Dictionary<string, Item>();

    public virtual void ProcessCSVRow(string[] values)
    {
        if(values.Length >= 5)
        {
            string name = values[0];
            Seed seed = new Seed();
            seed.LoadData(values);
            itemDatabase.Add(name, seed);
        }
    }

    public virtual void LoadItemsFromCSVFile(string filePath)
    {
        if(!File.Exists(filePath))
        {
            Debug.Log($"Error: CSV file not found at {filePath}");
            return;
        }
        string[] lines = File.ReadAllLines(filePath);
        for(int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if(string.IsNullOrEmpty(line)) continue;
            string[] values = line.Split(',');
            ProcessCSVRow(values);
        }
    }
    public Item GetItem(string itemName)
    {
        if(itemDatabase.TryGetValue(itemName, out Item item)) return item;
        Debug.Log($"Item not found: {itemName}");
        return null;
    }

    public T GetItem<T>(string itemName) where T: Item{
        if(itemDatabase.TryGetValue(itemName, out Item item) && item is T typedItem)
        {
            return typedItem;
        }
        Debug.Log($"Item of type:{typeof(T).Name} not found: {itemName}");
        return null;
    }

    public Dictionary<string, Item> GetItemDatabase() => itemDatabase;

}
