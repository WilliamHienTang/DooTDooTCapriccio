using UnityEngine;
using TMPro;

public class ScoreComboText : MonoBehaviour {

    public TextMeshProUGUI score;
    public TextMeshProUGUI combo;

	// Update the score and combo
	void Update () {
        score.text = AddLeadingScoreZeros(PlayerPrefs.GetInt(Constants.score));
        combo.text = PlayerPrefs.GetInt(Constants.combo).ToString();
    }

    string AddLeadingScoreZeros(int score)
    {
        string scoreString = score.ToString();
        string zeros = "";

        int numZeros = Constants.scoreDigits - scoreString.Length;
        for (int i = 0; i < numZeros; i++)
        {
            zeros += "<color=#808080>0</color>"; // grey
        }
        return zeros + scoreString;
    }
}
