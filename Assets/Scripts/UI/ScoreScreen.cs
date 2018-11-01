using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreScreen : MonoBehaviour {

    public TextMeshProUGUI songName;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI perfects;
    public TextMeshProUGUI greats;
    public TextMeshProUGUI goods;
    public TextMeshProUGUI bads;
    public TextMeshProUGUI misses;
    public TextMeshProUGUI notesHit;
    public TextMeshProUGUI maxCombo;

    string song;
    string difficulty;

    // Use this for initialization
    IEnumerator Start()
    {
        InitPlayerPrefs();

        // play bgm and fade
        FindObjectOfType<AudioManager>().Play(Constants.kaiheitaiBGM);
        float fadeTime = 0.5f / GameObject.Find("ScoreScreenCanvas").GetComponent<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadMainMenu()
    {
        StopAudio();
        SceneManager.LoadScene(Constants.mainMenu);
    }

    public void ButtonAudio1()
    {
        FindObjectOfType<AudioManager>().Play(Constants.button1SFX);
    }

    public void ButtonAudio2()
    {
        FindObjectOfType<AudioManager>().Play(Constants.button1SFX);
    }

    void StopAudio()
    {
        string currentMusic = FindObjectOfType<AudioManager>().GetCurrentBGM();

        if (currentMusic != null)
        {
            FindObjectOfType<AudioManager>().Stop(currentMusic);
        }
    }

    void InitPlayerPrefs()
    {
        song = PlayerPrefs.GetString(Constants.selectedSong);
        difficulty = PlayerPrefs.GetString(Constants.difficulty);

        if (PlayerPrefs.GetInt(Constants.score) > PlayerPrefs.GetInt(song + difficulty + Constants.highScore))
        {
            PlayerPrefs.SetInt(song + difficulty + Constants.highScore, PlayerPrefs.GetInt(Constants.score));
        }

        if (PlayerPrefs.GetInt(Constants.combo) > PlayerPrefs.GetInt(song + difficulty + Constants.maxCombo))
        {
            PlayerPrefs.SetInt(song + difficulty + Constants.maxCombo, PlayerPrefs.GetInt(Constants.combo));
        }

        int noteCount = PlayerPrefs.GetInt(Constants.perfects) + PlayerPrefs.GetInt(Constants.greats) + PlayerPrefs.GetInt(Constants.goods) + PlayerPrefs.GetInt(Constants.bads) + PlayerPrefs.GetInt(Constants.misses);

        songName.text = PlayerPrefs.GetString(Constants.selectedSongTitle);
        difficultyText.text = difficulty.ToUpper();
        score.text = PlayerPrefs.GetInt(Constants.score).ToString();
        highScore.text = PlayerPrefs.GetInt(song + difficulty + Constants.highScore).ToString();
        perfects.text = PlayerPrefs.GetInt(Constants.perfects).ToString();
        greats.text = PlayerPrefs.GetInt(Constants.greats).ToString();
        goods.text = PlayerPrefs.GetInt(Constants.goods).ToString();
        bads.text = PlayerPrefs.GetInt(Constants.bads).ToString();
        misses.text = PlayerPrefs.GetInt(Constants.misses).ToString();
        notesHit.text = PlayerPrefs.GetInt(Constants.notesHit).ToString() + "/" + noteCount.ToString();
        maxCombo.text = PlayerPrefs.GetInt(song + difficulty + Constants.maxCombo).ToString();
    }
}
