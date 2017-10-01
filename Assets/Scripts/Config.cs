using System.Collections.Generic;
using System.IO;
using Assets.Scripts;
using Assets.Scripts.Models;
using UnityEngine;
using Newtonsoft.Json;

public class Config : MonoBehaviour
{
    const string filename = "config.json";

    private static string GetFilePath()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var filepath = Path.Combine(currentDir, filename);
        return filepath;
    }

    public static void Save(ConfigClass config)
    {
        var json = JsonConvert.SerializeObject(config);
        // save to json file
        var filepath = GetFilePath();
        if (!File.Exists(filepath)) File.Create(filepath);
        File.WriteAllText(filepath, json);
    }

    public static ConfigClass Load()
    {
        var filepath = GetFilePath();
        if (!File.Exists(filepath)) return new ConfigClass();

        var json = File.ReadAllText(filepath);
        var config = JsonConvert.DeserializeObject<ConfigClass>(json) ?? new ConfigClass();
        return config;
    }
}
