using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pregame : MonoBehaviour {

    float destroyTime = 5.0f;

    // Song images
    public Sprite soundscape;
    public Sprite takarajima;
    public Sprite tutti;

    // Set song image and difficulty bar color and start destroy coroutine
    void Start () {
        SetSongImage();
        SetDifficultyColor();
        Destroy(gameObject, destroyTime);
	}

    void SetSongImage()
    {
        string song = PlayerPrefs.GetString(Constants.selectedSong);
        Image songImage = transform.Find("SongImage").GetComponent<Image>();

        switch (song)
        {
            case Constants.soundscapeSong:
                songImage.sprite = soundscape;
                break;
            case Constants.takarajimaSong:
                songImage.sprite = takarajima;
                break;
            case Constants.tuttiSong:
                songImage.sprite = tutti;
                break;
            default:
                return;
        }
    }

    void SetDifficultyColor()
    {
        string difficulty = PlayerPrefs.GetString(Constants.difficulty);
        Image difficultyBar = transform.Find("SongNamePanel").Find("DifficultyBar").GetComponent<Image>();

        switch (difficulty)
        {
            case Constants.easy:
                difficultyBar.color = Constants.easyColor;
                break;
            case Constants.normal:
                difficultyBar.color = Constants.normalColor;
                break;
            case Constants.hard:
                difficultyBar.color = Constants.hardColor;
                break;
            case Constants.expert:
                difficultyBar.color = Constants.expertColor;
                break;
            default:
                break;
        }
    }
}
