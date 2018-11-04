using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreScreen : MonoBehaviour {

    public GameObject scoreCanvas;
    public TextMeshProUGUI songName;
    public TextMeshProUGUI difficultyText;
    public GameObject difficultyBar;
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
        scoreCanvas.GetComponent<AnimatePanel>().PlayAnimator();
        InitText();

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

    void InitText()
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
        SetDifficultyColor();
        score.text = AddLeadingScoreZeros(PlayerPrefs.GetInt(Constants.score));
        highScore.text = AddLeadingScoreZeros(PlayerPrefs.GetInt(song + difficulty + Constants.highScore));
        perfects.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.perfects));
        greats.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.greats));
        goods.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.goods));
        bads.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.bads));
        misses.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.misses));
        notesHit.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.notesHit)) + "/" + AddLeadingComboZeros(noteCount);
        maxCombo.text = AddLeadingComboZeros(PlayerPrefs.GetInt(song + difficulty + Constants.maxCombo));
    }

    string AddLeadingScoreZeros(int score)
    {
        string scoreString = score.ToString();
        string zeros = "";

        int numZeros = Constants.scoreDigits - scoreString.Length;
        for (int i = 0; i < numZeros; i++)
        {
            zeros += "<color=#808080>0</color>";
        }
        scoreString = zeros + scoreString;
        return scoreString;
    }

    string AddLeadingComboZeros(int combo)
    {
        string comboString = combo.ToString();
        string zeros = "";

        int numZeros = Constants.comboDigits - comboString.Length;
        for (int i = 0; i < numZeros; i++)
        {
            zeros += "<color=#808080>0</color>";
        }
        comboString = zeros + comboString;
        return comboString;
    }

    void SetDifficultyColor()
    {
        switch (difficulty)
        {
            case Constants.easy:
                difficultyBar.GetComponent<Image>().color = Constants.easyColor;
                break;
            case Constants.medium:
                difficultyBar.GetComponent<Image>().color = Constants.mediumColor;
                break;
            case Constants.hard:
                difficultyBar.GetComponent<Image>().color = Constants.hardColor;
                break;
            case Constants.expert:
                difficultyBar.GetComponent<Image>().color = Constants.expertColor;
                break;
            default:
                break;
        }
    }
}
