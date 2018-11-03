using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePanel : MonoBehaviour {

    [Header("ANIMATORS")]
    public Animator panelAnimator;

    [Header("ANIMATION STRINGS")]
    public string fadeInAnim;
    public string fadeOutAnim;

    [Header("SETTINGS")]
    private bool isOn = false;

    public void PlayAnimator()
    {
        if (isOn == true)
        {
            panelAnimator.Play(fadeOutAnim);
            isOn = false;
        }
        else if (isOn == false)
        {
            panelAnimator.Play(fadeInAnim);
            isOn = true;
        }
    }
}
