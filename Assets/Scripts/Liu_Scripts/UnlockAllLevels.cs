using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAllLevels : MonoBehaviour
{
    private LevelSelection sl;

    private void Start()
    {
        sl = FindObjectOfType<LevelSelection>();
    }

    public void PressUnlockAll()
    {
        for (int i = 0; i <= 2; i++)
        {
            InGameSaveManager.currentSaveData.currentLevel = "Level9";
            GameObject.FindObjectsOfType<LevelSelection>()[i].unlocked = true;
            //sl.UpdateLevelImage();
            //sl.UpdateLevelStatus();
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i <= 2; i++)
        {
            InGameSaveManager.currentSaveData.currentLevel = "Level1";
            GameObject.FindObjectsOfType<LevelSelection>()[i].unlocked = false;
            //sl.UpdateLevelImage();
            //sl.UpdateLevelStatus();
        }
    }

}
