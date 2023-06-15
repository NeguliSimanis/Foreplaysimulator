using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource mainAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip sexyBgMusic;
    public bool isSexyMusicOn = false;

    // SFX
    public AudioClip[] lickAudio;
    float lastSoundPlayTime = 0f;
    float soundCooldown = 1f;

    public void PlaySFX(SoundType soundType)
    {
        if (Time.time < lastSoundPlayTime + soundCooldown)
            return;
        lastSoundPlayTime = Time.time;
        AudioClip soundToPlay = lickAudio[0];
        switch (soundType)
        {
            case SoundType.Lick:
                int lickRoll = Random.Range(0, lickAudio.Length);
                soundToPlay = lickAudio[lickRoll];
                break;
        }

        sfxAudioSource.PlayOneShot(soundToPlay);

    }

   

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
