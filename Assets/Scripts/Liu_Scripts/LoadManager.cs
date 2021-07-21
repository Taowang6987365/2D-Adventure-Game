using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Rewired.Integration.UnityUI;
using Rewired.UI.ControlMapper;

public class LoadManager : MonoBehaviour
{
    public GameObject loadingScreen;
    
    public GameObject settingCanvas;
    public GameObject menuCanvas;
    [SerializeField] private GameObject volumeCanvas;
    [SerializeField] private Canvas levelCanvas;
    private AsyncOperation async;
    [SerializeField] private Slider progressbar;
    public RewiredEventSystem rewiredEventSystem;

    //Menu scene
    public LevelSelection levelSelection;
    public GameObject menuFirstBtn;
    
    //setting
    public GameObject settingFirstBtn;
    public GameObject settingBackFirstBtn;
    
    //levelSelect
    public GameObject levelSelectFirstBtnOption1;
    public GameObject levelSelectFirstBtnOption2;
    public GameObject levelSelectBackFirstBtn;
    
    //volume
    public GameObject volumeFirstBtn;
    public GameObject volumeBackFirstBtn;
    
    //volume Controller
    [Header("Volume Controller") ]
    public Slider volumeSlider;
    public AudioSource menuBGM;
    
    //Input Setting
    public ControlMapper controlMapper;
    
    EventSystem eventSystem;


    void Start()
    {
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
        volumeCanvas.SetActive(false);
        menuCanvas.SetActive(true);

        progressbar.value = 0;
        eventSystem = rewiredEventSystem;
    }

    void Update()
    {
        DefaultBtnSelect();
    }

    IEnumerator LoadLevel(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");//load target scene
        menuCanvas.SetActive(false);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressbar.value = progress;
            yield return null;
        }
    }

    public void NextScene()
    {
        GameManager.startingLevel = "Level1";
        StartCoroutine(LoadLevel("Game"));

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettingPanel()
    {
        menuCanvas.SetActive(false);
        settingCanvas.SetActive(true);
        eventSystem.SetSelectedGameObject(settingFirstBtn);
    }

    public void LoadData()
    {
        menuCanvas.SetActive(false);
    }

    public void LevelSettingBackToMenu()
    {
        menuCanvas.SetActive(true);
        levelCanvas.gameObject.SetActive(false);
        eventSystem.SetSelectedGameObject(levelSelectBackFirstBtn);
    }

    public void CloseSettingPanel()
    {
        menuCanvas.SetActive(true);
        settingCanvas.SetActive(false);
        eventSystem.SetSelectedGameObject(settingBackFirstBtn);
    }
    
    
    public void SelevtLevelCanvas()
    {
        menuCanvas.gameObject.SetActive(false);
        levelCanvas.gameObject.SetActive(true);
        Debug.Log(levelSelectFirstBtnOption1.activeInHierarchy);
        if(!levelSelection.bFirstChapter)
        {
            eventSystem.SetSelectedGameObject(levelSelectFirstBtnOption2);
        }
        else
        {
            eventSystem.SetSelectedGameObject(levelSelectFirstBtnOption1);
        }
    }

    public void OpenVolumeCanvas()
    {
        volumeCanvas.SetActive(true);
        settingCanvas.SetActive(false);
        eventSystem.SetSelectedGameObject(volumeFirstBtn);
    }
    public void CloseVolumeCanvas()
    {
        volumeCanvas.SetActive(false);
        settingCanvas.SetActive(true);
        eventSystem.SetSelectedGameObject(volumeBackFirstBtn);
    }

    public void OpenInputSetting()
    {
        controlMapper.Open();
    }

    void DefaultBtnSelect()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            if (menuCanvas.activeSelf)
            {
                eventSystem.SetSelectedGameObject(menuFirstBtn);
            }
            else if (settingCanvas.activeSelf)
            {
                eventSystem.SetSelectedGameObject(settingFirstBtn);
            }
            else if (volumeCanvas.activeSelf)
            {
                eventSystem.SetSelectedGameObject(volumeFirstBtn);
            }
            else if (levelCanvas.enabled)
            {
                eventSystem.SetSelectedGameObject(menuFirstBtn);
            }
        }
    }

}
