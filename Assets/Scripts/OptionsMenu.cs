using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public AudioMixer audioMixer;
    public Slider SongSlider;
    public Slider GameSFXSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;

    public void SetSongVolume(float volume)
    {
        PlayerPrefs.SetFloat("SongVolume", volume);
        audioMixer.SetFloat("SongVolume", PlayerPrefs.GetFloat("SongVolume"));
        PlayerPrefs.Save();
    }

    public void SetGameSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("GameSFXVolume", volume);
        audioMixer.SetFloat("GameSFXVolume", PlayerPrefs.GetFloat("GameSFXVolume"));
        PlayerPrefs.Save();
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGMVolume", volume);
        audioMixer.SetFloat("BGMVolume", PlayerPrefs.GetFloat("BGMVolume"));
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        PlayerPrefs.Save();
    }

    // Use this for initialization
    void Start () {
        SongSlider.value = PlayerPrefs.GetFloat("SongVolume", SongSlider.value);
        GameSFXSlider.value = PlayerPrefs.GetFloat("GameSFXVolume", GameSFXSlider.value);
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume", BGMSlider.value);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", SFXSlider.value);
        audioMixer.SetFloat("SongVolume", PlayerPrefs.GetFloat("SongVolume"));
        audioMixer.SetFloat("GameSFXVolume", PlayerPrefs.GetFloat("GameSFXVolume"));
        audioMixer.SetFloat("BGMVolume", PlayerPrefs.GetFloat("BGMVolume"));
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        PlayerPrefs.Save();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
