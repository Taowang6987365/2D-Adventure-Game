using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class InGameSaveManager : MonoBehaviour
{
    public static InGameSaveManager instance;
    //public InGameSaveData activeSave;
    public bool isLoaded;
    public LevelName levelName;
    [SerializeField] private GameObject playerObject;
    public static InGameSaveData currentSaveData = new InGameSaveData();

    private void Awake()
    {
        instance = this;
        Load();
    }

    void Update()
    {
        //Debug.Log(currentSaveData.currentLevel);
        //for test
        if (Input.GetKeyDown(KeyCode.K))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            DeletSaveData();
        }


    }

    /*
    public void Save()
    {
        string dataPath = Application.persistentDataPath;//data saved address

        var serializaer = new XmlSerializer(typeof(InGameSaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);// location of the file
        serializaer.Serialize(stream, activeSave);//object that data save;
        stream.Close();//finish saving

        Debug.Log("Saved");
    }
    */

    /*public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializaer = new XmlSerializer(typeof(InGameSaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializaer.Deserialize(stream) as InGameSaveData;
            stream.Close();

            isLoaded = true;
        }
    }*/
    
    
    
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
            //Debug.LogError($"Save error... {e.Message}");
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
                currentSaveData = bf.Deserialize(file) as InGameSaveData;
                file.Close();
                if (currentSaveData == null)
                {
                    Debug.LogError("Found file and loaded it but it's not valid...");
                    currentSaveData = new InGameSaveData();
                }
            }
            catch (Exception e)
            {
                //Debug.LogError($"Load error... {e.Message}");
            }
            finally
            {
                //Debug.Log("Load successful.");
                if (file != null)
                {
                    file.Close();
                }
            }
        }
    }

    public void DeletSaveData()
    {
        if(File.Exists(SavePath))
        {
            currentSaveData = null;
            File.Delete(SavePath);
            
            Debug.Log("Deleted!");
        }
    }
}

[System.Serializable]
public class InGameSaveData
{
    public string saveName;

    public Vector3 respawnPosition;

    public bool canSave;

    public int playerLives;

    public string currentLevel;

}

public enum LevelName
{
    Level1,
    Level2,
    Level3
}


