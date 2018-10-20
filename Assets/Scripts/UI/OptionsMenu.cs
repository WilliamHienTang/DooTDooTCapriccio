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

        if (PlayerPrefs.GetFloat(Constants.noteSpeed) > 10.0f)
        {
            PlayerPrefs.SetFloat(Constants.noteSpeed, 1.0f);
            noteSpeed.SetText("1.0");
        }
        else if (PlayerPrefs.GetFloat(Constants.noteSpeed) < 1.0f)
        {
            PlayerPrefs.SetFloat(Constants.noteSpeed, 10.0f);
            noteSpeed.SetText("10.0");
        }
        else
        {
            noteSpeed.SetText(PlayerPrefs.GetFloat(Constants.noteSpeed).ToString("F1"));
        }

        PlayerPrefs.Save();
    }

    float RoundToTenth(float number)
    {
        number *= 10;
        number = Mathf.Round(number);
        return number / 10;
    }

    // Use this for initialization
    void Start () {
        noteSpeed.text = PlayerPrefs.GetFloat(Constants.noteSpeed).ToString();
        SongSlider.value = PlayerPrefs.GetFloat(Constants.songVolume, SongSlider.value);
        GameSFXSlider.value = PlayerPrefs.GetFloat(Constants.gameSFXVolume, GameSFXSlider.value);
        BGMSlider.value = PlayerPrefs.GetFloat(Constants.BGMVolume, BGMSlider.value);
        SFXSlider.value = PlayerPrefs.GetFloat(Constants.SFXVolume, SFXSlider.value);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
