using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

public class PlayerDataManager
{
    public void Save(PlayerData data, string filePath)
    {
        data.SetLastPlayedTime(DateTime.Now);
        string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, jsonString);
    }
    public PlayerData Load(string filePath)
    {
        if(!File.Exists(filePath)) return PlayerData.CreateAndInit();
        string jsonString = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<PlayerData>(jsonString);
    }
    public string SaveTest(PlayerData data, string filePath)
    {
        data.SetLastPlayedTime(DateTime.Now);
        string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
        return jsonString;
    }
}
