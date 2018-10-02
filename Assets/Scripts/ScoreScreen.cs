using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour {

    public void LoadMainMenu()
    {
        StopAudio();
        SceneManager.LoadScene("SongSelect");
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
        string currentMusic = FindObjectOfType<AudioManager>().GetCurrentMusic();

        if (currentMusic != null)
        {
            FindObjectOfType<AudioManager>().Stop(currentMusic);
        }
    }

	// Use this for initialization
	IEnumerator Start () {
        FindObjectOfType<AudioManager>().Play("kaiheitai");
        float fadeTime = 0.5f / GameObject.Find("ScoreScreenCanvas").GetComponent<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
