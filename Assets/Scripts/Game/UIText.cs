using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour {

    public TextMeshProUGUI score;
    public TextMeshProUGUI combo;

	// Update the score and combo
	public void UpdateScoreText () {
        score.text = AddLeadingScoreZeros(PlayerPrefs.GetInt(Constants.score));
    }

    public void UpdateComboText(){
        combo.text = PlayerPrefs.GetInt(Constants.combo).ToString();
    }

    string AddLeadingScoreZeros(int scoreInt)
    {
        string scoreString = scoreInt.ToString();
        string zeros = "";

        int numZeros = Constants.scoreDigits - scoreString.Length;
        for (int i = 0; i < numZeros; i++)
        {
            zeros += "<color=#808080>0</color>"; // grey
        }
        return zeros + scoreString;
    }
}
