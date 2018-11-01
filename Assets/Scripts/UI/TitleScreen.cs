using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class TitleScreen : MonoBehaviour {

    public TextMeshProUGUI tapText;
    int touches;
    bool enableTouch = true;

    IEnumerator Start()
    {
        //DefaultSettings();
        enabled = false;
        FindObjectOfType<AudioManager>().Play(Constants.vivaceBGM);
        float fadeTime = 0.5f / FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
        touches = 0;
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        touches = Input.touchCount;
        if ((touches > 0 || Input.GetMouseButtonDown(0)) && enableTouch)
        {
            enableTouch = false;
            StartCoroutine(BlinkText(8));
        }
    }

    // Start game after n text blinks
    IEnumerator BlinkText(int n)
    {
        tapText.GetComponent<Animator>().enabled = false;

        BlinkAudio();
        for (int i = 0; i < n; i++)
        {
            Color c = tapText.color;
            tapText.color = new Color(c.r, c.g, c.b, 0);
            yield return new WaitForSeconds(0.075f);
            tapText.color = new Color(c.r, c.g, c.b, 1);
            yield return new WaitForSeconds(0.075f);
        }
        StartGame();
    }

    void BlinkAudio()
    {
        FindObjectOfType<AudioManager>().Play(Constants.tapScreenSFX);
    }
    
    void StopMusic()
    {
        string currentMusic = FindObjectOfType<AudioManager>().GetCurrentBGM();

        if (currentMusic != null)
        {
            FindObjectOfType<AudioManager>().Stop(currentMusic);
        }
    }

    void DefaultSettings()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat(Constants.noteSpeed, 7.5f);
        PlayerPrefs.SetString(Constants.difficulty, Constants.easy);
    }

    void StartGame()
    {
        StopMusic();
        SceneManager.LoadScene(Constants.mainMenu);
    }
}
