using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScreenFade : MonoBehaviour {

    public Image background;
    public Text text;
    public string loadLevel;
    int touches;

    IEnumerator Start()
    {
        enabled = false;
        background.canvasRenderer.SetAlpha(0.0f);
        text.canvasRenderer.SetAlpha(0.0f);
        FadeInImage();
        yield return new WaitForSeconds(0.75f);
        FadeInText();
        yield return new WaitForSeconds(0.75f);
        touches = 0;
        enabled = true;
    }

    void FadeInImage()
    {
        background.CrossFadeAlpha(1.0f, 0.75f, false);
    }
	
    void FadeInText()
    {
        text.CrossFadeAlpha(1.0f, 0.75f, false);
    }

	// Update is called once per frame
	void Update () {
        touches = Input.touchCount;
        if (touches > 0 || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(loadLevel);
        }
	}
}
