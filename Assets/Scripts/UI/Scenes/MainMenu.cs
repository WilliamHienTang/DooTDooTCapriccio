using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    AudioManager audioManager;
    string BGM;

    IEnumerator Start()
    {
        // Play BGM
        audioManager = FindObjectOfType<AudioManager>();
        BGM = Constants.kimiBGM;
        audioManager.Play(BGM);

        // Fade in scene
        float fadeTime = FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }

    public void LoadSongScreen()
    {
        audioManager.StopBGM();
        SceneManager.LoadScene(Constants.songSelect);
    }

    public void LoadTitleScreen()
    {
        audioManager.StopBGM();
        SceneManager.LoadScene(Constants.titleScreen);
    }
}
