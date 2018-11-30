using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHandler : MonoBehaviour {


    [Header("PANEL LIST")]
    public List<GameObject> panels = new List<GameObject>();

    [Header("BUTTON LIST")]
    public List<GameObject> buttons = new List<GameObject>();

    [Header("PANEL ANIMS")]
    public string panelFadeIn = "Panel Open";
    public string panelFadeOut = "Panel Close";

    [Header("BUTTON ANIMS")]
    public string buttonFadeIn = "TP Open";
    public string buttonFadeOut = "TP Close";

    private GameObject currentPanel;
    private GameObject nextPanel;

    private GameObject currentButton;
    private GameObject nextButton;

    [Header("SETTINGS")]
    public int currentPanelIndex = 0;
    public int currentButtonIndex = 0;
    int newIndex;

    private Animator currentPanelAnimator;
    private Animator nextPanelAnimator;

    private Animator currentButtonAnimator;
    private Animator nextButtonAnimator;

    void Start ()
    {
        currentButton = buttons[currentButtonIndex];
        currentButtonAnimator = currentButton.GetComponent<Animator>();
        currentButtonAnimator.Play(buttonFadeIn);
    }

    public void PanelAnim (int newIndex)
    {
        this.newIndex = newIndex;
    }

    void Update()
    {
        if (newIndex != currentPanelIndex)
        {
            // panel anim
            currentPanel = panels[currentPanelIndex];

            currentPanelIndex = newIndex;
            nextPanel = panels[currentPanelIndex];

            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            nextPanelAnimator = nextPanel.GetComponent<Animator>();

            currentPanelAnimator.Play(panelFadeOut);
            nextPanelAnimator.Play(panelFadeIn);

            // button anim
            currentButton = buttons[currentButtonIndex];

            currentButtonIndex = newIndex;
            nextButton = buttons[currentButtonIndex];

            currentButtonAnimator = currentButton.GetComponent<Animator>();
            nextButtonAnimator = nextButton.GetComponent<Animator>();

            currentButtonAnimator.Play(buttonFadeOut);
            nextButtonAnimator.Play(buttonFadeIn);
        }
    }
}
