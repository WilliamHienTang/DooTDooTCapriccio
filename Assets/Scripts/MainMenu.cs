using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Image background;

    public void LoadSongScreen()
    {
        SceneManager.LoadScene("ScoreScreen");
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ButtonAudio()
    {
        FindObjectOfType<AudioManager>().Play("bubble2");
    }

    public void StopAudio()
    {
        FindObjectOfType<AudioManager>().Stop("Kimi");
    }

    public void BlurBackground()
    {
        background.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    }

    public void SharpenBackground()
    {
        background.color = new Color(1.0f, 1.0f, 1.0f,1.0f);
    }

	// Use this for initialization
	void Start () {
        FindObjectOfType<AudioManager>().Play("Kimi");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
