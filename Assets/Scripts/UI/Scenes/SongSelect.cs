using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongSelect : MonoBehaviour {

    AudioManager audioManager;

    public GameObject difficultyPanel;

    public GameObject RankSS;
    public GameObject RankS;
    public GameObject RankA;
    public GameObject RankB;
    public GameObject RankC;
    public GameObject RankF;

    IEnumerator Start()
    {
        InitDifficultyPanel();
        InitNoteCounts();
        audioManager = FindObjectOfType<AudioManager>();

        // Fade in scene
        float fadeTime = FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    // Display the corresponding obtained rank of the selected song at the selected difficulty
    void Update()
    {
        string song = PlayerPrefs.GetString(Constants.selectedSong);
        string difficulty = PlayerPrefs.GetString(Constants.difficulty);
        string rank = PlayerPrefs.GetString(song + difficulty + Constants.highRank);

        switch (rank)
        {
            case "SS":
                RankSS.SetActive(true);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
            case "S":
                RankSS.SetActive(false);
                RankS.SetActive(true);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
            case "A":
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(true);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
            case "B":
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(true);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
            case "C":
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(true);
                RankF.SetActive(false);
                break;
            case "F":
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(true);
                break;
            default:
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
        }
    }

    public void LoadGame()
    {
        audioManager.StopBGM();
        SceneManager.LoadScene(Constants.game);
    }

    public void LoadMainMenu()
    {
        audioManager.StopBGM();
        SceneManager.LoadScene(Constants.mainMenu);
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

    void InitNoteCounts()
    {
        PlayerPrefs.SetInt("soundscapeEasyNoteCount", Constants.soundscapeEasyNoteCount);
        PlayerPrefs.SetInt("soundscapeNormalNoteCount", Constants.soundscapeNormalNoteCount);
        PlayerPrefs.SetInt("soundscapeHardNoteCount", Constants.soundscapeHardNoteCount);
        PlayerPrefs.SetInt("soundscapeExpertNoteCount", Constants.soundscapeExpertNoteCount);
        PlayerPrefs.SetInt("takarajimaEasyNoteCount", Constants.takarajimaEasyNoteCount);
        PlayerPrefs.SetInt("takarajimaNormalNoteCount", Constants.takarajimaNormalNoteCount);
        PlayerPrefs.SetInt("takarajimaHardNoteCount", Constants.takarajimaHardNoteCount);
        PlayerPrefs.SetInt("takarajimaExpertNoteCount", Constants.takarajimaExpertNoteCount);
        PlayerPrefs.SetInt("tuttiEasyNoteCount", Constants.tuttiEasyNoteCount);
        PlayerPrefs.SetInt("tuttiNormalNoteCount", Constants.tuttiNormalNoteCount);
        PlayerPrefs.SetInt("tuttiHardNoteCount", Constants.tuttiHardNoteCount);
        PlayerPrefs.SetInt("tuttiExpertNoteCount", Constants.tuttiExpertNoteCount);
    }
}
