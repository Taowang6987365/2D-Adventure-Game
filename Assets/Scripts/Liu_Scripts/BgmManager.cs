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
        if (GameManager.startingLevel == "Level1" || GameManager.startingLevel == "Level2")
        {
           playSound(0);
        }      
        if (GameManager.startingLevel == "Level4" || GameManager.startingLevel == "Level5")
        {
           playSound(1);
        }      
        if (GameManager.startingLevel == "Level7" || GameManager.startingLevel == "Level8")
        {
           playSound(2);
        }

        if (GameManager.startingLevel == "Level3" || GameManager.startingLevel == "Level6" ||
            GameManager.startingLevel == "Level9")
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
