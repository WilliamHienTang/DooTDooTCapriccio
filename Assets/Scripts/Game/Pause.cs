using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    Transform gameCanvas;
    AudioManager audioManager;

    public GameObject countdown1;
    public GameObject countdown2;
    public GameObject countdown3;
    public GameObject pauseButton;

    bool isPaused = false;

    void Start()
    {
        gameCanvas = transform;
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(WaitEnablePauseButton());
    }

    public void PauseGame()
    {
        AudioListener.pause = true;
        audioManager.Pause(PlayerPrefs.GetString(Constants.selectedSong));
        isPaused = true;
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        StartCoroutine(CountdownResume());
    }

    // Resume game after 3s countdown
    public IEnumerator CountdownResume()
    {
        Instantiate(countdown3, new Vector3(gameCanvas.position.x, gameCanvas.position.y + 50.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas);
        yield return new WaitForSeconds(1);
        Instantiate(countdown2, new Vector3(gameCanvas.position.x, gameCanvas.position.y + 50.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas);
        yield return new WaitForSeconds(1);
        Instantiate(countdown1, new Vector3(gameCanvas.position.x, gameCanvas.position.y + 50.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas);
        yield return new WaitForSeconds(1);

        isPaused = false;
        AudioListener.pause = false;
        audioManager.Play(PlayerPrefs.GetString(Constants.selectedSong));
        pauseButton.SetActive(true);
    }

    IEnumerator WaitEnablePauseButton()
    {
        yield return new WaitForSeconds(8.0f);
        pauseButton.SetActive(true);
    }

    public void SetPauseButtonActive(bool active)
    {
        pauseButton.SetActive(active);
    }

    public void LoadSongSelect()
    {
        isPaused = false;
        AudioListener.pause = false;
        audioManager.Stop(PlayerPrefs.GetString(Constants.selectedSong));
        SceneManager.LoadScene(Constants.songSelect);
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
