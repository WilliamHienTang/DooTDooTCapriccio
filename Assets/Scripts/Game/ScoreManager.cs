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

    int score;
    int combo;
    int maxCombo;
    int perfects;
    int greats;
    int goods;
    int bads;
    int misses;
    int notesHit;

    void Start()
    {
        canvasObjectPooler = FindObjectOfType<CanvasObjectPooler>();
        scoreBar = FindObjectOfType<ScoreBar>();
        UIText = FindObjectOfType<UIText>();
        floatingTexts = new List<GameObject>();
        InitPlayerPrefs();
    }

    // Reset score, combo, and note related player prefs
    void InitPlayerPrefs()
    {
        song = PlayerPrefs.GetString(Constants.selectedSong);
        difficulty = PlayerPrefs.GetString(Constants.difficulty);
        PlayerPrefs.SetInt(Constants.score, 0);
        PlayerPrefs.SetInt(Constants.maxCombo, 0);
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
        // Update score/note variables
        score += points;
        notesHit++;

        switch(scoreTypeCount)
        {
            case Constants.perfects:
                perfects++;
                break;
            case Constants.greats:
                greats++;
                break;
            case Constants.goods:
                goods++;
                break;
            case Constants.bads:
                bads++;
                break;
        }

        // Spawn/update visuals
        SpawnFloatingText(scoreTypeCount);
        SpawnIncreaseScoreText(points);
        UIText.UpdateScoreText(score);
        scoreBar.FillScoreBar(score);
    }

    public void IncreaseCombo()
    {
        combo++;
        UIText.UpdateComboText(combo);
    }

    public void ResetCombo()
    {
        if (combo > maxCombo)
        {
            maxCombo = combo;
        }

        combo = 0;
        UIText.UpdateComboText(combo);
    }

    // Instantiates miss floating text
    public void MissNote()
    {
        misses++;
        SpawnFloatingText(Constants.misses);
    }

    // To be used after the end of the song
    public void SetPlayerPrefs(){
        PlayerPrefs.SetInt(Constants.score, score);
        PlayerPrefs.SetInt(Constants.perfects, perfects);
        PlayerPrefs.SetInt(Constants.greats, greats);
        PlayerPrefs.SetInt(Constants.goods, goods);
        PlayerPrefs.SetInt(Constants.bads, bads);
        PlayerPrefs.SetInt(Constants.misses, misses);
        PlayerPrefs.SetInt(Constants.notesHit, notesHit);

        SetMaxCombo();
        SetHighScore();
        SetScoreRank();
        SetHighRank();
    }

    void SetMaxCombo(){
        if (combo > maxCombo)
        {
            maxCombo = combo;
        }
        PlayerPrefs.SetInt(Constants.maxCombo, maxCombo);
    }

    void SetHighScore()
    {
        if (score > PlayerPrefs.GetInt(song + difficulty + Constants.highScore))
        {
            PlayerPrefs.SetInt(song + difficulty + Constants.highScore, score);
        }
    }

    void SetScoreRank()
    {
        int noteCount = PlayerPrefs.GetInt(song + difficulty + Constants.noteCount);
        float rank = (float)PlayerPrefs.GetInt(Constants.score) / (float)(noteCount * Constants.perfectScore);

        if (perfects == noteCount)
        {
            PlayerPrefs.SetString(Constants.scoreRank, "SS");
        }
        else if (perfects + greats == noteCount)
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
