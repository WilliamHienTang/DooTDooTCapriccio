using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public Transform gameCanvas;

    // Floating text
    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI greatText;
    public TextMeshProUGUI goodText;
    public TextMeshProUGUI badText;
    public TextMeshProUGUI missText;
    public TextMeshProUGUI increaseScoreText;
    List<TextMeshProUGUI> floatingTexts;

    string song;
    string difficulty;

    void Start()
    {
        floatingTexts = new List<TextMeshProUGUI>();
        song = PlayerPrefs.GetString(Constants.selectedSong);
        difficulty = PlayerPrefs.GetString(Constants.difficulty);
    }

    // Increases score and instantiates score type floating text and increase score floating text
    public void IncreaseScore(int points, string scoreTypeCount)
    {
        InstantiateFloatingText(scoreTypeCount);
        PlayerPrefs.SetInt(Constants.score, PlayerPrefs.GetInt(Constants.score) + points);
        PlayerPrefs.SetInt(Constants.notesHit, PlayerPrefs.GetInt(Constants.notesHit) + 1);
        PlayerPrefs.SetInt(scoreTypeCount, PlayerPrefs.GetInt(scoreTypeCount) + 1);
        TextMeshProUGUI increaseScoreInstance = Instantiate(increaseScoreText, new Vector3(gameCanvas.position.x, gameCanvas.position.y + 130.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas);
        increaseScoreInstance.text = "+" + points.ToString();
    }

    public void IncreaseCombo()
    {
        PlayerPrefs.SetInt(Constants.combo, PlayerPrefs.GetInt(Constants.combo) + 1);
    }

    public void ResetCombo()
    {
        if (PlayerPrefs.GetInt(Constants.combo) > PlayerPrefs.GetInt(song + difficulty + Constants.maxCombo))
        {
            PlayerPrefs.SetInt(song + difficulty + Constants.maxCombo, PlayerPrefs.GetInt(Constants.combo));
        }

        PlayerPrefs.SetInt(Constants.combo, 0);
    }

    // Instantiates miss floating text
    public void MissNote()
    {
        InstantiateFloatingText(Constants.misses);
        PlayerPrefs.SetInt(Constants.misses, PlayerPrefs.GetInt(Constants.misses) + 1);
    }

    void InstantiateFloatingText(string scoreTypeCount)
    {
        ClearFloatingText();

        switch (scoreTypeCount)
        {
            case Constants.perfects:
                floatingTexts.Add(Instantiate(perfectText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 25.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas));
                break;
            case Constants.greats:
                floatingTexts.Add(Instantiate(greatText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 25.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas));
                break;
            case Constants.goods:
                floatingTexts.Add(Instantiate(goodText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 25.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas));
                break;
            case Constants.bads:
                floatingTexts.Add(Instantiate(badText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 25.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas));
                break;
            case Constants.misses:
                floatingTexts.Add(Instantiate(missText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 25.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas));
                break;
            default:
                break;
        }
    }

    void ClearFloatingText()
    {
        foreach (TextMeshProUGUI floatingText in floatingTexts)
        {
            if (floatingText != null)
            {
                Destroy(floatingText.gameObject);
            }
        }
    }
}
