using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Rewired.Integration.UnityUI;

public class GameManager : MonoBehaviour
{
    public GameObject PausePannel;
    public GameObject FirstPauseBtn;
    public Canvas settingCanvas;
    bool isPaused;
    private LanguageHandler lang;
  

    public static string startingLevel = "Level1";
    
    //public string nextLevel;
    [NonSerialized] public static string currentLevel = "";
    bool isLevelLoaded;

    public bool isWin = false;
    public RewiredEventSystem rewiredEventSystem;
    EventSystem eventSystem;

    public GameObject volumeFirstBtn;
    public GameObject volumeBackFirstBtn;
    
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        PausePannel.SetActive(false);
        isPaused = false;
        currentLevel = startingLevel;
        LoadLevel(currentLevel);
        lang = LanguageHandler.instance;
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
        if(rewiredEventSystem == null)
        {
            rewiredEventSystem = GameObject.Find("Rewired Event System").GetComponent<RewiredEventSystem>();
            eventSystem = rewiredEventSystem;
        }
        
        if(PlayerController.playerControllerInstance.Player.GetButtonDown("Menu") && !isPaused)
        {
            PausePannel.SetActive(true);
            eventSystem.SetSelectedGameObject(FirstPauseBtn);
            Time.timeScale = 0;
            isPaused = true;
        }
        else if(PlayerController.playerControllerInstance.Player.GetButtonDown("Menu") && isPaused)
        {
            BackToGame();
        }
    }
    
    public void BackToGame()
    {
        PausePannel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
    public void BackToMenu()
    {
        isPaused = false;
        Time.timeScale = 1;
        Destroy(lang.gameObject);
        SceneManager.LoadScene("Menu");
        
    }

    public void OpenSettingCanvas()
    {
        PausePannel.SetActive(false);
        settingCanvas.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(volumeFirstBtn);
    }

    public void BackToPause()
    {
        settingCanvas.gameObject.SetActive(false);
        PausePannel.SetActive(true);
        eventSystem.SetSelectedGameObject(volumeBackFirstBtn);
    }
}
