using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongSelect : MonoBehaviour {

    public void LoadGame()
    {
        SceneManager.LoadScene("ScoreScreen");
    }

    public void LoadMainMenu()
    {
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

    // Use this for initialization
    IEnumerator Start () {
        float fadeTime = 0.5f / GameObject.Find("SongSelectCanvas").GetComponent<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
