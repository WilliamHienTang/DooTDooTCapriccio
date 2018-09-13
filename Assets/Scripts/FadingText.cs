using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingText : MonoBehaviour {
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        //Debug.Log(text.text);
        StartFading();
	}
	
    IEnumerator Fade()
    {
        while (true)
        {
            switch (text.color.a.ToString())
            {
                case "0":
                    StartCoroutine(FadeTextToFullAlpha(1.0f, text));
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    StartCoroutine(FadeTextToZeroAlpha(1.0f, text));
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    public IEnumerator FadeTextToFullAlpha(float time, Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float time, Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }

    void StartFading()
    {
        StopCoroutine("Fade");
        StartCoroutine("Fade");
    }

    void StopFading()
    {
        StopCoroutine("Fade");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
