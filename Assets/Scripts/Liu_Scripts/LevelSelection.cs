using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked;//default is false
    public Image unlockImage;

    private void Update()
    {
        UpdateLevelImage();
    }

    public void UpdateLevelImage()
    {
        if (!unlocked)//unlock is false, player cannot play this level
        {
            unlockImage.gameObject.SetActive(true);
        }
        else//if unlock is true, player can play this level
        {
            unlockImage.gameObject.SetActive(true);
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
