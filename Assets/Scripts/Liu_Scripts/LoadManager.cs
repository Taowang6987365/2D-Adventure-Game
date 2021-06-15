using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadingScreen;
    private AsyncOperation async;
    [SerializeField] private Image progressbar;
    public GameObject settingCanvas;
    public GameObject menuCanvas;


    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.SetActive(true);
        progressbar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator LoadLevel(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");//load target scene
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressbar.fillAmount = progress;
            yield return null;
        }
    }

    public void NextScene()
    {
        StartCoroutine(LoadLevel("Game"));

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettingPanel()
    {
        menuCanvas.SetActive(false);
        settingCanvas.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        menuCanvas.SetActive(true);
        settingCanvas.SetActive(false);
    }
}
