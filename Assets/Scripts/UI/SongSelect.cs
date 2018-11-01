using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongSelect : MonoBehaviour {

    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button expertButton;

    Color easyColor;
    Color mediumColor;
    Color hardColor;
    Color expertColor;

    // Use this for initialization
    IEnumerator Start()
    {
        easyColor = easyButton.GetComponent<Image>().color;
        mediumColor = mediumButton.GetComponent<Image>().color;
        hardColor = hardButton.GetComponent<Image>().color;
        expertColor = expertButton.GetComponent<Image>().color;
        SetDifficultyButton();

        float fadeTime = 0.5f / FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        StopMusic();
        SceneManager.LoadScene(Constants.game);
    }

    public void LoadMainMenu()
    {
        StopMusic();
        SceneManager.LoadScene(Constants.mainMenu);
    }

    public void ButtonAudio1()
    {
        FindObjectOfType<AudioManager>().Play(Constants.button1SFX);
    }

    public void ButtonAudio2()
    {
        FindObjectOfType<AudioManager>().Play(Constants.button2SFX);
    }

    public void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                PlayerPrefs.SetString(Constants.difficulty, "easy");
                break;
            case 2:
                PlayerPrefs.SetString(Constants.difficulty, "medium");
                break;
            case 3:
                PlayerPrefs.SetString(Constants.difficulty, "hard");
                break;
            case 4:
                PlayerPrefs.SetString(Constants.difficulty, "expert");
                break;
            default:
                break;
        }

        SetDifficultyButton();
    }

    void SetDifficultyButton()
    {
        Color grey = Color.grey;

        switch (PlayerPrefs.GetString(Constants.difficulty))
        {
            case Constants.easy:
                easyButton.GetComponent<Image>().color = easyColor;
                mediumButton.GetComponent<Image>().color = grey;
                hardButton.GetComponent<Image>().color = grey;
                expertButton.GetComponent<Image>().color = grey;
                break;

            case Constants.medium:
                easyButton.GetComponent<Image>().color = grey;
                mediumButton.GetComponent<Image>().color = mediumColor;
                hardButton.GetComponent<Image>().color = grey;
                expertButton.GetComponent<Image>().color = grey;
                break;

            case Constants.hard:
                easyButton.GetComponent<Image>().color = grey;
                mediumButton.GetComponent<Image>().color = grey;
                hardButton.GetComponent<Image>().color = hardColor;
                expertButton.GetComponent<Image>().color = grey;
                break;

            case Constants.expert:
                easyButton.GetComponent<Image>().color = grey;
                mediumButton.GetComponent<Image>().color = grey;
                hardButton.GetComponent<Image>().color = grey;
                expertButton.GetComponent<Image>().color = expertColor;
                break;
            default:
                break;
        }
    }

    void StopMusic()
    {
        string currentMusic = FindObjectOfType<AudioManager>().GetCurrentBGM();

        if (currentMusic != null)
        {
            FindObjectOfType<AudioManager>().Stop(currentMusic);
        }
    }
}
