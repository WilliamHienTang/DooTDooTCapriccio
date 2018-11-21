using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongSelect : MonoBehaviour {

    public GameObject difficultyPanel;
    public GameObject SSRank;
    public GameObject SRank;
    public GameObject ARank;
    public GameObject BRank;
    public GameObject CRank;
    public GameObject FRank;

    // Use this for initialization
    IEnumerator Start()
    {
        InitDifficultyPanel();

        float fadeTime = 0.5f / FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    // Update is called once per frame
    void Update()
    {
        string song = PlayerPrefs.GetString(Constants.selectedSong);
        string difficulty = PlayerPrefs.GetString(Constants.difficulty);

        switch (PlayerPrefs.GetString(song + difficulty + Constants.highRank))
        {
            case "SS":
                SSRank.SetActive(true);
                SRank.SetActive(false);
                ARank.SetActive(false);
                BRank.SetActive(false);
                CRank.SetActive(false);
                FRank.SetActive(false);
                break;
            case "S":
                SSRank.SetActive(false);
                SRank.SetActive(true);
                ARank.SetActive(false);
                BRank.SetActive(false);
                CRank.SetActive(false);
                FRank.SetActive(false);
                break;
            case "A":
                SSRank.SetActive(false);
                SRank.SetActive(false);
                ARank.SetActive(true);
                BRank.SetActive(false);
                CRank.SetActive(false);
                FRank.SetActive(false);
                break;
            case "B":
                SSRank.SetActive(false);
                SRank.SetActive(false);
                ARank.SetActive(false);
                BRank.SetActive(true);
                CRank.SetActive(false);
                FRank.SetActive(false);
                break;
            case "C":
                SSRank.SetActive(false);
                SRank.SetActive(false);
                ARank.SetActive(false);
                BRank.SetActive(false);
                CRank.SetActive(true);
                FRank.SetActive(false);
                break;
            case "F":
                SSRank.SetActive(false);
                SRank.SetActive(false);
                ARank.SetActive(false);
                BRank.SetActive(false);
                CRank.SetActive(false);
                FRank.SetActive(true);
                break;
            default:
                SSRank.SetActive(false);
                SRank.SetActive(false);
                ARank.SetActive(false);
                BRank.SetActive(false);
                CRank.SetActive(false);
                FRank.SetActive(false);
                break;
        }
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

    public void ButtonAudio3()
    {
        FindObjectOfType<AudioManager>().Play(Constants.button3SFX);
    }

    public void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                PlayerPrefs.SetString(Constants.difficulty, Constants.easy);
                break;
            case 2:
                PlayerPrefs.SetString(Constants.difficulty, Constants.normal);
                break;
            case 3:
                PlayerPrefs.SetString(Constants.difficulty, Constants.hard);
                break;
            case 4:
                PlayerPrefs.SetString(Constants.difficulty, Constants.expert);
                break;
            default:
                break;
        }
    }

    void InitDifficultyPanel()
    {
        string difficulty = PlayerPrefs.GetString(Constants.difficulty);

        switch (difficulty)
        {
            case Constants.easy:
                difficultyPanel.GetComponent<PanelHandler>().PanelAnim(0);
                break;
            case Constants.normal:
                difficultyPanel.GetComponent<PanelHandler>().PanelAnim(1);
                break;
            case Constants.hard:
                difficultyPanel.GetComponent<PanelHandler>().PanelAnim(2);
                break;
            case Constants.expert:
                difficultyPanel.GetComponent<PanelHandler>().PanelAnim(3);
                break;
            default:
                break;
        }
        difficultyPanel.GetComponent<AnimatePanel>().PlayAnimator();
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
