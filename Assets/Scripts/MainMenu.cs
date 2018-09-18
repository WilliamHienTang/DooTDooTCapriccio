using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void LoadSongScreen()
    {
        StopAudio();
        SceneManager.LoadScene("ScoreScreen");
    }

    public void LoadTitleScreen()
    {
        StopAudio();
        SceneManager.LoadScene("TitleScreen");
    }

    public void ButtonAudio()
    {
        FindObjectOfType<AudioManager>().Play("bubble2");
    }

    void StopAudio()
    {
        FindObjectOfType<AudioManager>().Stop("Kimi");
    }

	// Use this for initialization
	void Start () {
        FindObjectOfType<AudioManager>().Play("Kimi");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
