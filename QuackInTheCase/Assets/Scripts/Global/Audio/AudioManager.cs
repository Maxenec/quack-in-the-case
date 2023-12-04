using UnityEngine.Audio;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] music, sfx;
    public AudioSource musicSource, sfxSource;

    private string bgMusic;
    private const string musicVolumeKey = "MusicVolume";
    private const string sfxVolumeKey = "MusicVolume";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        
    }

    public void PlayMusic(string name)
    {
        bgMusic = name;
        Sound sound = Array.Find(music, x => x.name == bgMusic);

        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
        else
        {
            Debug.Log("Music audio missing.");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        Sound sound = Array.Find(music, x => x.name == bgMusic);

        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
        else
        {
            Debug.Log("Music audio missing.");
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfx, x => x.name == name);

        if (sound != null)
        {
            sfxSource.clip = sound.clip;
            sfxSource.Play();
        }
        else
        {
            Debug.Log("SFX audio missing.");
        }
    }

    //To turn off and on the sounds from player end.
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(musicVolumeKey, volume);
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(sfxVolumeKey, volume);
    }

    
}
