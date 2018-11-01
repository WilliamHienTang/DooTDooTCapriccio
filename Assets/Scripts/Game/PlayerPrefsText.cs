using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPrefsText : MonoBehaviour {

    public TextMeshProUGUI score;
    public TextMeshProUGUI combo;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        string scoreString = PlayerPrefs.GetInt(Constants.score).ToString();
        string comboString = PlayerPrefs.GetInt(Constants.combo).ToString();
        string zeros = "";

        int numZeros = Constants.scoreDigits - scoreString.Length;
        for (int i = 0; i < numZeros; i++)
        {
            zeros += "0";
        }
        score.text = zeros + scoreString;

        combo.text = comboString;
    }
}
