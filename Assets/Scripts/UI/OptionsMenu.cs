using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour {

    public Button GameplaySettingsTab;
    public Button GameSoundSettingsTab;
    public Button SystemSettingsTab;
    public AudioMixer audioMixer;
    public Slider SongSlider;
    public Slider GameSFXSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public TextMeshProUGUI noteSpeed;

    // Use this for initialization
    void Start()
    {
        SetTab(1);
        noteSpeed.text = PlayerPrefs.GetFloat(Constants.noteSpeed).ToString();
        SongSlider.value = PlayerPrefs.GetFloat(Constants.songVolume, SongSlider.value);
        GameSFXSlider.value = PlayerPrefs.GetFloat(Constants.gameSFXVolume, GameSFXSlider.value);
        BGMSlider.value = PlayerPrefs.GetFloat(Constants.BGMVolume, BGMSlider.value);
        SFXSlider.value = PlayerPrefs.GetFloat(Constants.SFXVolume, SFXSlider.value);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTab(int tabIndex)
    {
        Color color1 = GameplaySettingsTab.GetComponent<Image>().color;
        Color color2 = GameSoundSettingsTab.GetComponent<Image>().color;
        Color color3 = SystemSettingsTab.GetComponent<Image>().color;

        switch (tabIndex)
        {
            case 1:
                GameplaySettingsTab.GetComponent<Image>().color = new Color(color1.r, color1.g, color1.b, 1f);
                GameSoundSettingsTab.GetComponent<Image>().color = new Color(color2.r, color2.g, color2.b, 0.5f);
                SystemSettingsTab.GetComponent<Image>().color = new Color(color3.r, color3.g, color3.b, 0.5f);
                break;
            case 2:
                GameplaySettingsTab.GetComponent<Image>().color = new Color(color1.r, color1.g, color1.b, 0.5f);
                GameSoundSettingsTab.GetComponent<Image>().color = new Color(color2.r, color2.g, color2.b, 1f);
                SystemSettingsTab.GetComponent<Image>().color = new Color(color3.r, color3.g, color3.b, 0.5f);
                break;
            case 3:
                GameplaySettingsTab.GetComponent<Image>().color = new Color(color1.r, color1.g, color1.b, 0.5f);
                GameSoundSettingsTab.GetComponent<Image>().color = new Color(color2.r, color2.g, color2.b, 0.5f);
                SystemSettingsTab.GetComponent<Image>().color = new Color(color3.r, color3.g, color3.b, 1f);
                break;
            default:
                break;
        }
    }

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

        PlayerPrefs.Save();
    }

    float RoundToTenth(float number)
    {
        number *= 10;
        number = Mathf.Round(number);
        return number / 10;
    }
}
