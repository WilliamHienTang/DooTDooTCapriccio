using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongSelect : MonoBehaviour {

    public GameObject difficultyPanel;

    // Use this for initialization
    IEnumerator Start()
    {
        difficultyPanel.GetComponent<AnimatePanel>().PlayAnimator();

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

    void StopMusic()
    {
        string currentMusic = FindObjectOfType<AudioManager>().GetCurrentBGM();

        if (currentMusic != null)
        {
            FindObjectOfType<AudioManager>().Stop(currentMusic);
        }
    }
}
