using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour {

    Slider scoreBarSlider;
    Image fill;
    Color colorA;
    Color colorB;
    Color colorC;
    Color colorF;

    float score;
    float maxScore;
    string song;
    string difficulty;
    float scoreRank;

	void Start () {
        scoreBarSlider = transform.GetComponent<Slider>();
        fill = transform.Find("FillArea").Find("Fill").GetComponent<Image>();
        colorA = new Color(1, 0, 0);
        colorB = new Color(0, 1, 0);
        colorC = new Color(1, 50f/255f, 0);
        colorF = new Color(40f/255f, 40f/255f, 40f/255f);

        song = PlayerPrefs.GetString(Constants.selectedSong);
        difficulty = PlayerPrefs.GetString(Constants.difficulty);
        maxScore = (float)PlayerPrefs.GetInt(song + difficulty + Constants.noteCount) * Constants.perfectScore;
    }
	
	// Fill the score bar based on the score
	public void FillScoreBar (float score) {
        scoreRank = score / maxScore;
        scoreBarSlider.value = scoreRank;

        fill.color = scoreRank > Constants.rankA ? colorA : scoreRank > Constants.rankB ? colorB : scoreRank > Constants.rankC ? colorC : colorF;
    }
}
