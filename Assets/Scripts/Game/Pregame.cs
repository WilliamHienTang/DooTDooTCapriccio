using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pregame : MonoBehaviour {

    readonly float destroyTime = 5.0f;

    // Song images
    public Sprite soundscape;
    public Sprite takarajima;
    public Sprite tutti;

    // Set song image, song name panel and start destroy coroutine
    void Start () {
        SetSongImage();
        SetSongNamePanel();
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

    void SetSongNamePanel()
    {
        Transform songNamePanel = transform.Find("SongNamePanel");
        Image difficultyBar = songNamePanel.Find("DifficultyBar").GetComponent<Image>();

        string difficulty = PlayerPrefs.GetString(Constants.difficulty);

        // Set difficulty color
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

        // Set difficulty text
        TextMeshProUGUI difficultyText = songNamePanel.Find("DifficultyBar").Find("DifficultyText").GetComponent<TextMeshProUGUI>();
        difficultyText.text = difficulty.ToUpper();

        // Set song name text
        TextMeshProUGUI songNameText = songNamePanel.Find("SongNameText").GetComponent<TextMeshProUGUI>();
        songNameText.text = PlayerPrefs.GetString(Constants.selectedSongTitle);
    }
}
