using UnityEngine;

public class ButtonAudio : MonoBehaviour {

    AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void ButtonAudio1()
    {
        audioManager.Play(Constants.button1SFX);
    }

    public void ButtonAudio2()
    {
        audioManager.Play(Constants.button2SFX);
    }

    public void ButtonAudio3()
    {
        audioManager.Play(Constants.button3SFX);
    }
}
