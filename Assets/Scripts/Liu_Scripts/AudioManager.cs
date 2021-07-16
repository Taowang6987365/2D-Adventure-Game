using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetSFXVol(float sfxVl)
    {
        masterMixer.SetFloat("SFXVol",sfxVl);
    }

    public void SetMusicVol(float musicVl)
    {
        masterMixer.SetFloat("MusicVol", musicVl);
    }
    
}
