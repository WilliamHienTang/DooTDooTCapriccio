using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongSelect : MonoBehaviour {

    public void LoadGame()
    {
        StopMusic();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        StopMusic();
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonAudio1()
    {
        FindObjectOfType<AudioManager>().Play("bubble2");
    }

    public void ButtonAudio2()
    {
        FindObjectOfType<AudioManager>().Play("reward");
    }

    public void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                PlayerPrefs.SetString("Difficulty", "easy");
                break;
            case 2:
                PlayerPrefs.SetString("Difficulty", "medium");
                break;
            case 3:
                PlayerPrefs.SetString("Difficulty", "hard");
                break;
            case 4:
                PlayerPrefs.SetString("Difficulty", "expert");
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

    // Use this for initialization
    IEnumerator Start () {
        float fadeTime = 0.5f / FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
