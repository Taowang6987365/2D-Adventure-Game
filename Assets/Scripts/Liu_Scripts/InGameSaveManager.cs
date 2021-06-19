using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class InGameSaveManager : MonoBehaviour
{
    public static InGameSaveManager instance;
    public InGameSaveData activeSave;
    public bool isLoaded;
    public LevelName levelName;
    [SerializeField] private GameObject playerObject;

    private void Awake()
    {
        instance = this;
        Load();
    }

    void Update()
    {
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

    public void Save()
    {
        string dataPath = Application.persistentDataPath;//data saved address

        var serializaer = new XmlSerializer(typeof(InGameSaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);// location of the file
        serializaer.Serialize(stream, activeSave);//object that data save;
        stream.Close();//finish saving

        Debug.Log("Saved");
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializaer = new XmlSerializer(typeof(InGameSaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializaer.Deserialize(stream) as InGameSaveData;
            stream.Close();

            
            Debug.Log("Loaded");
            isLoaded = true;
        }
    }

    public void DeletSaveData()
    {
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
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


