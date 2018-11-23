using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelect : MonoBehaviour {

    AudioManager audioManager;

    IEnumerator Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        // Fade in scene
        float fadeTime = FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
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
}
