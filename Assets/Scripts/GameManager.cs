using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject PausePannel;
    public GameObject FirstPauseBtn;
    bool isPaused;

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
        PausePannel.SetActive(false);
        isPaused = false;

        currentLevel = startingLevel;
        if (currentLevel == "")
        {
            InGameSaveManager.instance.levelName = LevelName.Level1;
        }
        else if (InGameSaveManager.currentSaveData.currentLevel == "Level2")
        {
            currentLevel = InGameSaveManager.currentSaveData.currentLevel;
        }
        else if(InGameSaveManager.currentSaveData.currentLevel == "Level3")
        {
            currentLevel = InGameSaveManager.currentSaveData.currentLevel;
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
        InGameSaveManager.currentSaveData.currentLevel = currentLevel;
    }


    private void Update()
    {
        
        if(PlayerController.playerControllerInstance.Player.GetButtonDown("Menu") && !isPaused)
        {
            PausePannel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(FirstPauseBtn);
            Time.timeScale = 0;
            isPaused = true;
        }
        else if(PlayerController.playerControllerInstance.Player.GetButtonDown("Menu") && isPaused)
        {
            BackToGame();
        }
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void BackToGame()
    {
        PausePannel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
