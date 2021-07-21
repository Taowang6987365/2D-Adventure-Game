using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private float timer = 1f;
    private bool isFinished;
    public string sceneName;
    public string currentScene;


    private void Update()
    {
        if (BossFightController.GetInstance().isWin)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && !isFinished)
            {
                SceneManager.UnloadSceneAsync(currentScene);
                InGameSaveManager.currentSaveData.currentLevel = sceneName;
            }
        }
        else
        {
            isFinished = false;
        }
    }
}
