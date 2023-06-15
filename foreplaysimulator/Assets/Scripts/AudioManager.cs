using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource mainAudioSource;

    public AudioClip sexyBgMusic;
    public bool isSexyMusicOn = false;

    public void StartBGMusic()
    { 
        if(isSexyMusicOn)
        {
            return;
        }
        isSexyMusicOn = true;
        mainAudioSource.Play();
    }
}
