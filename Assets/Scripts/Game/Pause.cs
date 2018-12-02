using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    Transform gameCanvas;
    AudioManager audioManager;
    CanvasObjectPooler canvasObjectPooler;
    public GameObject pauseButton;

    bool isPaused;

    void Start()
    {
        gameCanvas = transform;
        audioManager = FindObjectOfType<AudioManager>();
        canvasObjectPooler = FindObjectOfType<CanvasObjectPooler>();
        StartCoroutine(WaitEnablePauseButton());
        isPaused = false;
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
        canvasObjectPooler.SpawnFromPool(Constants.countdownText3, new Vector3(gameCanvas.position.x, gameCanvas.position.y + 100.0f, gameCanvas.position.z), Quaternion.identity);
        yield return new WaitForSeconds(1);
        canvasObjectPooler.SpawnFromPool(Constants.countdownText2, new Vector3(gameCanvas.position.x, gameCanvas.position.y + 100.0f, gameCanvas.position.z), Quaternion.identity);
        yield return new WaitForSeconds(1);
        canvasObjectPooler.SpawnFromPool(Constants.countdownText1, new Vector3(gameCanvas.position.x, gameCanvas.position.y + 100.0f, gameCanvas.position.z), Quaternion.identity);
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
