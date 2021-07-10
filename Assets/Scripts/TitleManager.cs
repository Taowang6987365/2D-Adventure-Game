using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class TitleManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    public GameManager gm;

    //...

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Save()
    {
        InGameSaveManager.currentSaveData.saveName = inputField.text;
        //SaveManager.Save();
        InGameSaveManager.Save();
    }

    public void Load()
    {
        //SaveManager.Load();
        InGameSaveManager.Load();
        inputField.text = InGameSaveManager.currentSaveData.saveName;
    }
    
    public void StartChapterOne()
    {
        GameManager.startingLevel = "Level1";
        SceneManager.LoadScene("Game");
    }
    public void StartChapterTwo()
    {
        GameManager.startingLevel = "Level4";
        SceneManager.LoadScene("Game");

    }
    
    public void StartChapterThree()
    {
        GameManager.startingLevel = "Level7";
        SceneManager.LoadScene("Game");
    }
}
