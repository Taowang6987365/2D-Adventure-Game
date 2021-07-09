using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject settingCanvas;
    public GameObject menuCanvas;
    public GameObject dataCanvas;
    [SerializeField] private Canvas levelCanvas;
    private AsyncOperation async;
    [SerializeField] private Slider progressbar;

    //In the Menu scene
    public GameObject settingFirstBtn;
    public GameObject backFirstBtn;


    void Start()
    {
        menuCanvas.SetActive(true);
        dataCanvas.SetActive(false);
        progressbar.value = 0;
    }
    
    IEnumerator LoadLevel(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");//load target scene
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingFirstBtn);
    }

    public void LoadData()
    {
        menuCanvas.SetActive(false);
        dataCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        menuCanvas.SetActive(true);
        levelCanvas.gameObject.SetActive(false);
    }

    public void CloseSettingPanel()
    {
        menuCanvas.SetActive(true);
        settingCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backFirstBtn);
    }
    
    
    public void SelevtLevelCanvas()
    {
        menuCanvas.gameObject.SetActive(false);
        levelCanvas.gameObject.SetActive(true);
    }


}
