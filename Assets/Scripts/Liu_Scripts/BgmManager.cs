using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    private bool isDone;
    private bool isFinished;
    [SerializeField] private string previousLevel;
    [SerializeField] private string currentLevel;
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    private void Start()
    {
        previousLevel = "";
        currentLevel = "Level1";
    }

    private void Update()
    {
        if (currentLevel != previousLevel)
        {
            ResetAudio();
            previousLevel = currentLevel;
            isDone = false;
        }
        
        currentLevel = InGameSaveManager.currentSaveData.currentLevel;
        
        if (InGameSaveManager.currentSaveData.currentLevel == "Level1" ||InGameSaveManager.currentSaveData.currentLevel == "Level2")
        {
            playSound(0);
        }      
        if (InGameSaveManager.currentSaveData.currentLevel == "Level4" || InGameSaveManager.currentSaveData.currentLevel == "Level5")
        {
            playSound(1);
        }      
        if (InGameSaveManager.currentSaveData.currentLevel == "Level7" || InGameSaveManager.currentSaveData.currentLevel == "Level8")
        {
            playSound(2);
        }

        if (InGameSaveManager.currentSaveData.currentLevel == "Level3" || InGameSaveManager.currentSaveData.currentLevel == "Level6" ||
            InGameSaveManager.currentSaveData.currentLevel == "Level9")
        {
            playSound(3);
        }
    }

    private void ResetAudio()
    {
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.loop = false;
    }
    
    private void playSound(int soundNum)
    {
        if (!isDone)
        {
            isDone = true;
            audioSource.clip = audioClips[soundNum];
            audioSource.Play();
            audioSource.loop = true;
        }
    }
}
