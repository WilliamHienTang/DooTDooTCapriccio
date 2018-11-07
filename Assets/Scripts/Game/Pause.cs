using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject countdown1;
    public GameObject countdown2;
    public GameObject countdown3;

    GameObject gameCanvas;
    bool isPaused = false;

    // Use this for initialization
    void Start()
    {
        gameCanvas = GameObject.Find("GameCanvas");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResumeGame()
    {
        StartCoroutine(CountdownResume());
    }

    public IEnumerator CountdownResume()
    {
        Instantiate(countdown3, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y + 50.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
        yield return new WaitForSeconds(1);
        Instantiate(countdown2, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y + 50.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
        yield return new WaitForSeconds(1);
        Instantiate(countdown1, new Vector3(gameCanvas.transform.position.x, gameCanvas.transform.position.y + 50.0f, gameCanvas.transform.position.z), Quaternion.identity, gameCanvas.transform);
        yield return new WaitForSeconds(1);

        isPaused = false;
        AudioListener.pause = false;
        FindObjectOfType<AudioManager>().Play(PlayerPrefs.GetString(Constants.selectedSong));
    }

    public void PauseGame()
    {
        AudioListener.pause = true;
        FindObjectOfType<AudioManager>().Pause(PlayerPrefs.GetString(Constants.selectedSong));
        isPaused = true;
    }

    public void LoadSongSelect()
    {
        isPaused = false;
        AudioListener.pause = false;
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
