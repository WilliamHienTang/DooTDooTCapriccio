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
        SceneManager.LoadScene("SongSelect");
    }

    public void LoadTitleScreen()
    {
        StopMusic();
        SceneManager.LoadScene("TitleScreen");
    }

    public void ButtonAudio1()
    {
        FindObjectOfType<AudioManager>().Play("bubble2");
    }

    public void ButtonAudio2()
    {
        FindObjectOfType<AudioManager>().Play("reward");
    }

    void StopMusic()
    {
        string currentMusic = FindObjectOfType<AudioManager>().GetCurrentMusic();

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
        FindObjectOfType<AudioManager>().Play("kimi");
        float fadeTime = 0.5f / GameObject.Find("MainMenuCanvas").GetComponent<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
