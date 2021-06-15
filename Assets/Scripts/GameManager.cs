using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string startingLevel = "Level1";
    public string nextLevel;
    [NonSerialized] public static string currentLevel = "";
    bool isLevelLoaded;

    public bool isWin = false;


    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentLevel = startingLevel;
        //if (currentLevel == "")
        //{
        //    
        //    InGameSaveManager.instance.levelName = LevelName.Level1;
        //}
        //else 
        if (InGameSaveManager.instance.activeSave.currentLevel == "Level2")
        {
            currentLevel = InGameSaveManager.instance.activeSave.currentLevel;
        }
        else if(InGameSaveManager.instance.activeSave.currentLevel == "Level3")
        {
            currentLevel = InGameSaveManager.instance.activeSave.currentLevel;
        }

        LoadLevel(currentLevel);
        //LoadLevel(nextLevel);
    }

    public void LoadLevel(string levelName)
    {
        isWin = false;
        if (isLevelLoaded)
        {
            SceneManager.UnloadSceneAsync(currentLevel);
        }
        isLevelLoaded = true;
        currentLevel = levelName;
        SceneManager.LoadScene(currentLevel, LoadSceneMode.Additive);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}