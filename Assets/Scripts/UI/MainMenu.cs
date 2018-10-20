using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Image background;

    public void LoadSongScreen()
    {
        StopMusic();
        SceneManager.LoadScene(Constants.songSelect);
    }

    public void LoadTitleScreen()
    {
        StopMusic();
        SceneManager.LoadScene(Constants.titleScreen);
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

    void StopMusic()
    {
        string currentMusic = FindObjectOfType<AudioManager>().GetCurrentBGM();

        if (currentMusic != null)
        {
            FindObjectOfType<AudioManager>().Stop(currentMusic);
        }
    }

    public void BlurBackground()
    {
        background.color = new Color(0.2f, 0.2f, 0.2f, 1.0f);
    }

    public void SharpenBackground()
    {
        background.color = new Color(0.392f, 0.392f, 0.392f, 1.0f);
    }

	// Use this for initialization
	IEnumerator Start () {
        FindObjectOfType<AudioManager>().Play(Constants.kimiBGM);
        float fadeTime = 0.5f / FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
