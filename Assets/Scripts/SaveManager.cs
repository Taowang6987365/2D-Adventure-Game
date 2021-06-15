using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveData
{
    public string playerName;
}

public static class SaveManager
{
    public static SaveData currentSaveData = new SaveData();

    private static string SaveDirectory => Path.Combine(Application.persistentDataPath, "Local");

    private static string SavePath => Path.Combine(SaveDirectory, "PandaSave.bin");

    public static void Save()
    {
        try
        {
            var bf = new BinaryFormatter();
            Directory.CreateDirectory(SaveDirectory);
            var file = File.Create(SavePath);
            bf.Serialize(file, currentSaveData);
            file.Close();
            Debug.Log("Save successful.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Save error... {e.Message}");
        }
    }

    public static void Load()
    {
        if (File.Exists(SavePath))
        {
            FileStream file = null;
            try
            {
                var bf = new BinaryFormatter();
                file = File.Open(SavePath, FileMode.Open);
                currentSaveData = bf.Deserialize(file) as SaveData;
                file.Close();
                if (currentSaveData == null)
                {
                    Debug.LogError("Found file and loaded it but it's not valid...");
                    currentSaveData = new SaveData();
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Load error... {e.Message}");
            }
            finally
            {
                Debug.Log("Load successful.");
                if (file != null)
                {
                    file.Close();
                }
            }
        }
    }
}
