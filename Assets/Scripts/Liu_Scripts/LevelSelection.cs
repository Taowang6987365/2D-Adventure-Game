using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelection : MonoBehaviour
{
    public bool unlocked;//default is false
    public Image unlockImage;
    [SerializeField] private string chapterName; 

    private void Update()
    {
        UpdateLevelImage();
    }

    public void UpdateLevelStatus()
    {
        if (InGameSaveManager.currentSaveData.currentLevel == "Level4" && chapterName =="chapterOne")
        {
            unlocked = true;
        }
        if (InGameSaveManager.currentSaveData.currentLevel == "Level7" && chapterName =="chapterTwo")
        {
            unlocked = true;
        }
        if (InGameSaveManager.currentSaveData.currentLevel == "Level9" && chapterName =="chapterThree")
        {
            unlocked = true;
        }
    }
    

    public void UpdateLevelImage()
    {
        if (!unlocked)//unlock is false, player cannot play this level
        {
            unlockImage.gameObject.SetActive(true);
        }
        else//if unlock is true, player can play this level
        {
            unlockImage.gameObject.SetActive(false);
        }
        
    }

    public void PressSelection(string _levelName)//buttom to press, scene to go
    {
        if (unlocked)
        {
            SceneManager.LoadScene(_levelName);
        }
    }
    
}
