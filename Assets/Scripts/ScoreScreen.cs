using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour {

    public void LoadMainMenu()
    {
        StopAudio();
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonAudio1()
    {
        FindObjectOfType<AudioManager>().Play("bubble2");
    }

    public void ButtonAudio2()
    {
        FindObjectOfType<AudioManager>().Play("reward");
    }

    void StopAudio()
    {
        FindObjectOfType<AudioManager>().Stop("Kaiheitai");
    }

	// Use this for initialization
	void Start () {
        FindObjectOfType<AudioManager>().Play("Kaiheitai");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
