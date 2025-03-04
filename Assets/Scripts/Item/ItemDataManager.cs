using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Net.WebSockets;

public class ItemDataManager
{
    protected Dictionary<string, Item> itemDatabase = new Dictionary<string, Item>();

    private Dictionary<string, string[]> itemData = new Dictionary<string, string[]>();

    public virtual void ProcessCSVRow(string[] values)
    {
        string id = values[1];
        itemData.Add(id, values);
        Item item = CreateItem(id);
        itemDatabase.Add(id, item);
    }

    public virtual void LoadItemsFromCSVFile(string filePath)
    {
        if(!File.Exists(filePath))
        {
            Console.WriteLine($"Error: CSV file not found at {filePath}");
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

    public bool IsContain(string itemName) => itemDatabase.ContainsKey(itemName);

    public Item GetItem(string itemName)
    {
        if(itemDatabase.TryGetValue(itemName, out Item item))
        {
            return item.Clone();
        }
        Console.WriteLine($"Item not found: {itemName}");
        return null;
    }

    public T GetItem<T>(string itemId) where T: Item{
        if(itemDatabase.TryGetValue(itemId, out Item item) && item is T typedItem)
        {
            return typedItem.Clone<T>();
        }
        Console.WriteLine($"Item of type:{typeof(T).Name} not found: {itemId}");
        return null;
    }

    public T CreateItem<T>(string itemId) where T: Item{
        if(itemDatabase.TryGetValue(itemId, out Item item) && item is T typedItem)
        {
            return (T)CreateItem(itemId);
        }
        Console.WriteLine($"Item of type:{typeof(T).Name} not found: {itemId}");
        return null;
    }

    public Item CreateItem(string id)
    {
        if(!itemData.ContainsKey(id)) return null;
        string[] values = itemData[id];
        Item item = null;
        if(Enum.TryParse(values[0], out ItemType type))
        {
            if(type == ItemType.Seed)
            {
                Seed seed = new Seed();
                seed.LoadData(values);
                item = seed;
            }
            else if(type == ItemType.Animal)
            {
                Animal animal = new Animal();
                animal.LoadData(values);
                item = animal;
            }
            else if(type == ItemType.Product)
            {
                Product product = new Product();
                product.LoadData(values);
                item = product;
            }
        }
        return item;
    }


    public Dictionary<string, Item> GetItemDatabase() => itemDatabase;

}
