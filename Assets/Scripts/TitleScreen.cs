using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class TitleScreen : MonoBehaviour {

    public TextMeshProUGUI tapText;
    public string loadLevel;
    int touches;
    bool enableTouch = true;

    IEnumerator Start()
    {
        FindObjectOfType<AudioManager>().Play("Vivace");
        enabled = false;
        float fadeTime = 0.5f / GameObject.Find("TitleScreenCanvas").GetComponent<Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime);
        touches = 0;
        enabled = true;
    }

    // Start game after n text blinks
    IEnumerator BlinkText(int n)
    {
        BlinkAudio();

        for (int i = 0; i < n; i++)
        {
            Color c = tapText.color;
            tapText.color = new Color(c.r, c.g, c.b, 0);
            yield return new WaitForSeconds(0.05f);
            tapText.color = new Color(c.r, c.g, c.b, 1);
            yield return new WaitForSeconds(0.05f);
        }
        StartGame();
    }

    void BlinkAudio()
    {
        FindObjectOfType<AudioManager>().Play("skill_slot_open");
    }
    
    void StopAudio()
    {
        FindObjectOfType<AudioManager>().Stop("Vivace");
    }

    void StartGame()
    {
        StopAudio();
        SceneManager.LoadScene(loadLevel);
    }

    // Update is called once per frame
    void Update () {
        touches = Input.touchCount;
        if ((touches > 0 || Input.GetMouseButtonDown(0)) && enableTouch)
        {
            enableTouch = false;
            StartCoroutine(BlinkText(6));
        }
	}
}
