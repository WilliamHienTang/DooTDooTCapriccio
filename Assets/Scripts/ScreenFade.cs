using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScreenFade : MonoBehaviour {

    public Image background;
    public string loadLevel;
    int touches;

    IEnumerator Start()
    {
        enabled = false;
        background.canvasRenderer.SetAlpha(0.0f);
        FadeInImage();
        yield return new WaitForSeconds(2.5f);
        touches = 0;
        enabled = true;
    }

    void FadeInImage()
    {
        background.CrossFadeAlpha(1.0f, 1.5f, false);
    }
	
	// Update is called once per frame
	void Update () {
        touches = Input.touchCount;
        if (touches > 0)
        {
            SceneManager.LoadScene(loadLevel);
        }
	}
}
