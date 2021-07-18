using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{

    public AudioSource[] audioSource;

    private void Start()
    {
        audioSource = transform.GetComponentsInChildren<AudioSource>();
    }

    private void Update()
    {
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
        foreach (var sound in audioSource)
        {
            sound.playOnAwake = false;
            sound.loop = false;
        }
    }

    private void playSound(int soundNum)
    {
        ResetAudio();
        if (!audioSource[soundNum].isPlaying)
        {
            audioSource[soundNum].Play();
            audioSource[soundNum].playOnAwake = true;
            audioSource[soundNum].loop = true;
        }
    }
}
