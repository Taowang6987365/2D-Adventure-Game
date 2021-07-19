using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour
{
    public bool unlocked;//default is false
    public Image unlockImage;
    [SerializeField] private string chapterName;
    private Button bt;
    public bool bFirstChapter;

    private void Start()
    {
        unlocked = false;
        bt = GetComponent<Button>();
        bFirstChapter = false;
    }

    private void Update()
    {
        if (unlocked == false)
        {
            bt.interactable = false;
            UpdateLevelImage();
        }
        else
        {
            bt.interactable = true;
            UpdateLevelImage();
        }
        
    }

    public void UpdateLevelStatus()
    {
        if (InGameSaveManager.currentSaveData.currentLevel == "Level4" && chapterName =="chapterOne")
        {
            unlocked = true;
            bFirstChapter = true;
        }
        if (InGameSaveManager.currentSaveData.currentLevel == "Level7" && chapterName =="chapterTwo" || chapterName =="chapterOne")
        {
            unlocked = true;
        }
        if (InGameSaveManager.currentSaveData.currentLevel == "Level9" && chapterName =="chapterThree"|| chapterName =="chapterTwo" || chapterName =="chapterOne")
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
