using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public Texture2D fadeOutTexture; // Black fade texture

    float fadeSpeed = 1.0f;
    int drawDepth = -1000;
    float alpha = 1.0f;
    int fadeDir = -1;

    // Fade animation using fade texture
    public void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    // direction = -1: fade in; direction = 1: fade out 
    // Returns fade duration
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    // Fade in
    void OnLoad()
    {
        BeginFade(-1);
    }
}
