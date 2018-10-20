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
        score.text = PlayerPrefs.GetInt(Constants.score).ToString();
        combo.text = PlayerPrefs.GetInt(Constants.combo).ToString();
    }
}
