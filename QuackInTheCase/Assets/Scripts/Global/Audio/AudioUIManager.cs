using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUIManager : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    public void Start()
    {
        _musicSlider.value = AudioManager.Instance.SavedMusicVolume();
        _sfxSlider.value = AudioManager.Instance.SavedSFXVolume();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
