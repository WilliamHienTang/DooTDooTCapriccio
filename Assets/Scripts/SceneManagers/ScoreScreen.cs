using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour {

    AudioManager audioManager;

    // Use this for initialization
    IEnumerator Start()
    {
        // Play BGM
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(Constants.kaiheitaiBGM);

        // Fade in scene
        float fadeTime = FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    public void LoadMainMenu()
    {
        audioManager.StopBGM();
        SceneManager.LoadScene(Constants.mainMenu);
    }
}
