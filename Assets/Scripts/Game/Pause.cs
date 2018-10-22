using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    bool isPaused = false;
    public GameObject pauseMenuUI;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        AudioListener.pause = false;
        isPaused = false;
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play(PlayerPrefs.GetString(Constants.selectedSong));
    }

    public void PauseGame()
    {
        AudioListener.pause = true;
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().Pause(PlayerPrefs.GetString(Constants.selectedSong));
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    public void LoadSongSelect()
    {
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        StopMusic();
        SceneManager.LoadScene(Constants.songSelect);
    }

    void StopMusic()
    {
        FindObjectOfType<AudioManager>().Stop(PlayerPrefs.GetString(Constants.selectedSong));
    }

    public void ButtonAudio1()
    {
        FindObjectOfType<AudioManager>().Play(Constants.button1SFX);
    }

    public void ButtonAudio2()
    {
        FindObjectOfType<AudioManager>().Play(Constants.button2SFX);
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
