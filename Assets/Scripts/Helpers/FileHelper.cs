using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class FileHelper : MonoBehaviour {

    public static string GetFilePath(string pathname, string filename)
    {
        return Path.Combine(pathname, filename);
    }

    public static string GetCurrentFilePath(string filename)
    {
        var currentDir = Directory.GetCurrentDirectory();
        return GetFilePath(currentDir, filename);
    }

    public static void Save<T>(string filepath, T contents)
    {
        var json = JsonConvert.SerializeObject(contents);
        if (!File.Exists(filepath))
        {
            using (File.Create(filepath)) {}
        }
        File.WriteAllText(filepath, json);
    }

    public static T Load<T>(string filepath)
    {
        if (!File.Exists(filepath)) return default(T); //new T();
        var json = File.ReadAllText(filepath);
        return JsonConvert.DeserializeObject<T>(json); //?? new T();
    }

	public static T Convert<T>(string json)
	{
		return JsonConvert.DeserializeObject<T> (json);
	}
}
