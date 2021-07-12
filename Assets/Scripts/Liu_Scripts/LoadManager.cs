using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Rewired.Integration.UnityUI;

public class LoadManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject settingCanvas;
    public GameObject menuCanvas;
    public GameObject dataCanvas;
    [SerializeField] private Canvas levelCanvas;
    private AsyncOperation async;
    [SerializeField] private Slider progressbar;
    public RewiredEventSystem rewiredEventSystem;

    //In the Menu scene
    public LevelSelection levelSelection;
    public GameObject settingFirstBtn;
    public GameObject settingBackFirstBtn;
    public GameObject levelSelectFirstBtnOption1;
    public GameObject levelSelectFirstBtnOption2;
    public GameObject levelSelectBackFirstBtn;
    EventSystem eventSystem;
    

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuCanvas.SetActive(true);
        dataCanvas.SetActive(false);
        progressbar.value = 0;
        eventSystem = rewiredEventSystem;
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
        dataCanvas.SetActive(true);
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


}
