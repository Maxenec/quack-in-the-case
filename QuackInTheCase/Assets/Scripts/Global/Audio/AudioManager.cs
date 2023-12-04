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
    private const string sfxVolumeKey = "SFXVolume";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        MusicVolume(SavedMusicVolume());
        SFXVolume(SavedSFXVolume());
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

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(musicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(sfxVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public float SavedMusicVolume()
    {
        return PlayerPrefs.GetFloat(musicVolumeKey, 1.0f);
    }

    public float SavedSFXVolume()
    {
        return PlayerPrefs.GetFloat(sfxVolumeKey, 1.0f);
    }
}
