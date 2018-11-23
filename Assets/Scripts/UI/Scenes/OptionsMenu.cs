using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour {

    public AudioMixer audioMixer;
    public Slider SongSlider;
    public Slider GameSFXSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public TextMeshProUGUI noteSpeed;

    // Init UI values
    void Start()
    {
        noteSpeed.text = PlayerPrefs.GetFloat(Constants.noteSpeed).ToString();
        SongSlider.value = PlayerPrefs.GetFloat(Constants.songVolume, SongSlider.value);
        GameSFXSlider.value = PlayerPrefs.GetFloat(Constants.gameSFXVolume, GameSFXSlider.value);
        BGMSlider.value = PlayerPrefs.GetFloat(Constants.BGMVolume, BGMSlider.value);
        SFXSlider.value = PlayerPrefs.GetFloat(Constants.SFXVolume, SFXSlider.value);
        PlayerPrefs.Save();
    }

    // Slider value setting functions
    public void SetSongVolume(float volume)
    {
        PlayerPrefs.SetFloat(Constants.songVolume, volume);
        audioMixer.SetFloat(Constants.songVolume, PlayerPrefs.GetFloat(Constants.songVolume));
        PlayerPrefs.Save();
    }

    public void SetGameSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(Constants.gameSFXVolume, volume);
        audioMixer.SetFloat(Constants.gameSFXVolume, PlayerPrefs.GetFloat(Constants.gameSFXVolume));
        PlayerPrefs.Save();
    }

    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat(Constants.BGMVolume, volume);
        audioMixer.SetFloat(Constants.BGMVolume, PlayerPrefs.GetFloat(Constants.BGMVolume));
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(Constants.SFXVolume, volume);
        audioMixer.SetFloat(Constants.SFXVolume, PlayerPrefs.GetFloat(Constants.SFXVolume));
        PlayerPrefs.Save();
    }

    public void ChangeNoteSpeed(float speed)
    {
        float newSpeed = PlayerPrefs.GetFloat(Constants.noteSpeed) + speed;
        newSpeed = RoundToTenth(newSpeed);
        PlayerPrefs.SetFloat(Constants.noteSpeed, newSpeed);

        // Wrapping value around max and min note speed
        if (PlayerPrefs.GetFloat(Constants.noteSpeed) > Constants.maxNoteSpeed)
        {
            PlayerPrefs.SetFloat(Constants.noteSpeed, Constants.minNoteSpeed);
            noteSpeed.SetText(Constants.minNoteSpeed.ToString());
        }
        else if (PlayerPrefs.GetFloat(Constants.noteSpeed) < Constants.minNoteSpeed)
        {
            PlayerPrefs.SetFloat(Constants.noteSpeed, Constants.maxNoteSpeed);
            noteSpeed.SetText(Constants.maxNoteSpeed.ToString());
        }
        else
        {
            noteSpeed.SetText(PlayerPrefs.GetFloat(Constants.noteSpeed).ToString("F1"));
        }
    }

    float RoundToTenth(float number)
    {
        number *= 10;
        number = Mathf.Round(number);
        return number / 10;
    }
}
