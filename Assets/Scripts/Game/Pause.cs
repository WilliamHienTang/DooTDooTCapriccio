using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    bool isPaused = false;
    public GameObject pauseMenuUI;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play(PlayerPrefs.GetString("SelectedSong"));
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().Pause(PlayerPrefs.GetString("SelectedSong"));
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    public void LoadSongSelect()
    {
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        StopMusic();
        SceneManager.LoadScene("SongSelect");
    }

    void StopMusic()
    {
        FindObjectOfType<AudioManager>().Stop(PlayerPrefs.GetString("SelectedSong"));
    }

    public void ButtonAudio1()
    {
        FindObjectOfType<AudioManager>().Play("bubble2");
    }

    public void ButtonAudio2()
    {
        FindObjectOfType<AudioManager>().Play("reward");
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
