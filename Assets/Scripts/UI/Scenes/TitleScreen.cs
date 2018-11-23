using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class TitleScreen : MonoBehaviour {

    AudioManager audioManager;
    string BGM;
    public TextMeshProUGUI tapText;
    bool isTouchingDevice;

    void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                isTouchingDevice = false;
                break;
            default:
                isTouchingDevice = true;
                break;
        }
    }

    IEnumerator Start()
    {
        //DefaultSettings();
        enabled = false;

        // Play BGM
        audioManager = FindObjectOfType<AudioManager>();
        BGM = Constants.vivaceBGM;
        audioManager.Play(BGM);

        // Fade in scene
        float fadeTime = FindObjectOfType<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);

        enabled = true;
    }

    // Load main menu on screen touch
    void Update()
    {
        if (isTouchingDevice)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                enabled = false;
                StartCoroutine(LoadMainMenu(6));
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                enabled = false;
                StartCoroutine(LoadMainMenu(6));
            }
        }
    }

    // Load main menu after text blinks
    IEnumerator LoadMainMenu(int blinks)
    {
        tapText.GetComponent<Animator>().enabled = false;
        audioManager.Play(Constants.tapScreenSFX);

        // text blink by alternating alpha
        Color c = tapText.color;
        for (int i = 0; i < blinks; i++)
        {
            tapText.color = new Color(c.r, c.g, c.b, 0);
            yield return new WaitForSeconds(0.075f);
            tapText.color = new Color(c.r, c.g, c.b, 1);
            yield return new WaitForSeconds(0.075f);
        }

        audioManager.StopBGM();
        SceneManager.LoadScene(Constants.mainMenu);
    }

    // Reinit player prefs
    void DefaultSettings()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat(Constants.noteSpeed, 7.5f);
        PlayerPrefs.SetString(Constants.difficulty, Constants.easy);
        PlayerPrefs.SetString(Constants.selectedSong, Constants.soundscapeSong);
    }
}
