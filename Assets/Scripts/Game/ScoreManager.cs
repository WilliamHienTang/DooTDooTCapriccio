using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public Transform gameCanvas;
    CanvasObjectPooler canvasObjectPooler;
    ScoreBar scoreBar;
    UIText UIText;

    // Floating text
    public TextMeshProUGUI increaseScoreText;
    List<GameObject> floatingTexts;

    string song;
    string difficulty;

    void Start()
    {
        canvasObjectPooler = FindObjectOfType<CanvasObjectPooler>();
        scoreBar = FindObjectOfType<ScoreBar>();
        UIText = FindObjectOfType<UIText>();
        floatingTexts = new List<GameObject>();
        song = PlayerPrefs.GetString(Constants.selectedSong);
        difficulty = PlayerPrefs.GetString(Constants.difficulty);
        InitPlayerPrefs();
    }

    // Reset score and combo related player prefs
    void InitPlayerPrefs()
    {
        PlayerPrefs.SetInt(Constants.score, 0);
        PlayerPrefs.SetInt(Constants.combo, 0);
        PlayerPrefs.SetInt(song + difficulty + Constants.maxCombo, 0);
        PlayerPrefs.SetInt(Constants.perfects, 0);
        PlayerPrefs.SetInt(Constants.greats, 0);
        PlayerPrefs.SetInt(Constants.goods, 0);
        PlayerPrefs.SetInt(Constants.bads, 0);
        PlayerPrefs.SetInt(Constants.misses, 0);
        PlayerPrefs.SetInt(Constants.notesHit, 0);
    }

    // Increases score and instantiates score type floating text and increase score floating text
    public void IncreaseScore(int points, string scoreTypeCount)
    {
        SpawnFloatingText(scoreTypeCount);
        PlayerPrefs.SetInt(Constants.score, PlayerPrefs.GetInt(Constants.score) + points);
        PlayerPrefs.SetInt(Constants.notesHit, PlayerPrefs.GetInt(Constants.notesHit) + 1);
        PlayerPrefs.SetInt(scoreTypeCount, PlayerPrefs.GetInt(scoreTypeCount) + 1);
        SpawnIncreaseScoreText(points);
        UIText.UpdateScoreText();
        scoreBar.FillScoreBar();
    }

    public void IncreaseCombo()
    {
        PlayerPrefs.SetInt(Constants.combo, PlayerPrefs.GetInt(Constants.combo) + 1);
        UIText.UpdateComboText();
    }

    public void ResetCombo()
    {
        if (PlayerPrefs.GetInt(Constants.combo) > PlayerPrefs.GetInt(song + difficulty + Constants.maxCombo))
        {
            PlayerPrefs.SetInt(song + difficulty + Constants.maxCombo, PlayerPrefs.GetInt(Constants.combo));
        }

        PlayerPrefs.SetInt(Constants.combo, 0);
        UIText.UpdateComboText();
    }

    // Instantiates miss floating text
    public void MissNote()
    {
        SpawnFloatingText(Constants.misses);
        PlayerPrefs.SetInt(Constants.misses, PlayerPrefs.GetInt(Constants.misses) + 1);
    }

    // To be used after the end of the song
    public void SetScoreRank()
    {
        int noteCount = PlayerPrefs.GetInt(song + difficulty + Constants.noteCount);
        float rank = (float)PlayerPrefs.GetInt(Constants.score) / (float)(noteCount * Constants.perfectScore);

        if (PlayerPrefs.GetInt(Constants.perfects) == noteCount)
        {
            PlayerPrefs.SetString(Constants.scoreRank, "SS");
        }
        else if (PlayerPrefs.GetInt(Constants.perfects) + PlayerPrefs.GetInt(Constants.greats) == noteCount)
        {
            PlayerPrefs.SetString(Constants.scoreRank, "S");
        }
        else if (rank >= Constants.rankA)
        {
            PlayerPrefs.SetString(Constants.scoreRank, "A");
        }
        else if (rank >= Constants.rankB)
        {
            PlayerPrefs.SetString(Constants.scoreRank, "B");
        }
        else if (rank >= Constants.rankC)
        {
            PlayerPrefs.SetString(Constants.scoreRank, "C");
        }
        else
        {
            PlayerPrefs.SetString(Constants.scoreRank, "F");
        }

        SetHighRank();
    }

    public void SetHighRank()
    {
        int scoreRank = 0;
        int highRank = 0;

        switch (PlayerPrefs.GetString(Constants.scoreRank))
        {
            case "SS":
                scoreRank = 6;
                break;
            case "S":
                scoreRank = 5;
                break;
            case "A":
                scoreRank = 4;
                break;
            case "B":
                scoreRank = 3;
                break;
            case "C":
                scoreRank = 2;
                break;
            case "F":
                scoreRank = 1;
                break;
        }

        switch (PlayerPrefs.GetString(song + difficulty + Constants.highRank))
        {
            case "SS":
                highRank = 6;
                break;
            case "S":
                highRank = 5;
                break;
            case "A":
                highRank = 4;
                break;
            case "B":
                highRank = 3;
                break;
            case "C":
                highRank = 2;
                break;
            case "F":
                scoreRank = 1;
                break;
        }

        if (scoreRank > highRank)
        {
            PlayerPrefs.SetString(song + difficulty + Constants.highRank, PlayerPrefs.GetString(Constants.scoreRank));
        }
    }

    // Score type text
    void SpawnFloatingText(string scoreTypeCount)
    {
        ClearFloatingText();

        switch (scoreTypeCount)
        {
            case Constants.perfects:
                floatingTexts.Add(canvasObjectPooler.SpawnFromPool(Constants.perfectText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 150.0f, gameCanvas.position.z), Quaternion.identity));
                break;
            case Constants.greats:
                floatingTexts.Add(canvasObjectPooler.SpawnFromPool(Constants.greatText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 150.0f, gameCanvas.position.z), Quaternion.identity));
                break;
            case Constants.goods:
                floatingTexts.Add(canvasObjectPooler.SpawnFromPool(Constants.goodText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 150.0f, gameCanvas.position.z), Quaternion.identity));
                break;
            case Constants.bads:
                floatingTexts.Add(canvasObjectPooler.SpawnFromPool(Constants.badText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 150.0f, gameCanvas.position.z), Quaternion.identity));
                break;
            case Constants.misses:
                floatingTexts.Add(canvasObjectPooler.SpawnFromPool(Constants.missText, new Vector3(gameCanvas.position.x, gameCanvas.position.y - 150.0f, gameCanvas.position.z), Quaternion.identity));
                break;
        }
    }


    // Score bar side text
    void SpawnIncreaseScoreText(int points){
        TextMeshProUGUI increaseScoreInstance = Instantiate(increaseScoreText, new Vector3(gameCanvas.position.x, gameCanvas.position.y + 520.0f, gameCanvas.position.z), Quaternion.identity, gameCanvas);
        increaseScoreInstance.text = "+" + points.ToString();
    }

    public void ClearFloatingText()
    {
        foreach (GameObject floatingText in floatingTexts)
        {
            if (floatingText != null)
            {
                floatingText.SetActive(false);
            }
        }
    }
}
