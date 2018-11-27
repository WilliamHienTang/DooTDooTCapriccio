using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCanvas : MonoBehaviour {

    string song;
    string difficulty;

    void Start () {
        song = PlayerPrefs.GetString(Constants.selectedSong);
        difficulty = PlayerPrefs.GetString(Constants.difficulty);

        transform.GetComponent<AnimatePanel>().PlayAnimator();
        SetSongNamePanel();
        SetScorePanel();
        SetScoreRank();
    }

    void SetSongNamePanel()
    {
        Transform songNamePanel = transform.Find("Content").Find("SongNamePanel");
        TextMeshProUGUI songNameText = songNamePanel.Find("SongNameText").GetComponent<TextMeshProUGUI>();
        Image difficultyBar = songNamePanel.Find("DifficultyBar").GetComponent<Image>();
        TextMeshProUGUI difficultyText = songNamePanel.Find("DifficultyBar").Find("DifficultyText").GetComponent<TextMeshProUGUI>();

        songNameText.text = PlayerPrefs.GetString(Constants.selectedSongTitle);
        difficultyText.text = difficulty.ToUpper();

        switch (difficulty)
        {
            case Constants.easy:
                difficultyBar.color = Constants.easyColor;
                break;
            case Constants.normal:
                difficultyBar.color = Constants.normalColor;
                break;
            case Constants.hard:
                difficultyBar.color = Constants.hardColor;
                break;
            case Constants.expert:
                difficultyBar.color = Constants.expertColor;
                break;
            default:
                break;
        }
    }

    void SetScorePanel()
    {
        SetHighScore();
        SetMaxCombo();
        int noteCount = PlayerPrefs.GetInt(Constants.perfects) + PlayerPrefs.GetInt(Constants.greats) + PlayerPrefs.GetInt(Constants.goods) + PlayerPrefs.GetInt(Constants.bads) + PlayerPrefs.GetInt(Constants.misses);

        Transform scorePanel = transform.Find("Content").Find("ScorePanel");
        TextMeshProUGUI score = scorePanel.Find("ScoreText").Find("Score").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI highScore = scorePanel.Find("HighScoreText").Find("HighScore").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI perfects = scorePanel.Find("PerfectText").Find("Perfects").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI greats = scorePanel.Find("GreatText").Find("Greats").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI goods = scorePanel.Find("GoodText").Find("Goods").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bads = scorePanel.Find("BadText").Find("Bads").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI misses = scorePanel.Find("MissText").Find("Misses").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI notesHit = scorePanel.Find("NotesHitText").Find("NotesHit").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI maxCombo = scorePanel.Find("ComboBox").Find("MaxCombo").GetComponent<TextMeshProUGUI>();

        score.text = AddLeadingScoreZeros(PlayerPrefs.GetInt(Constants.score));
        highScore.text = AddLeadingScoreZeros(PlayerPrefs.GetInt(song + difficulty + Constants.highScore));
        perfects.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.perfects));
        greats.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.greats));
        goods.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.goods));
        bads.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.bads));
        misses.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.misses));
        notesHit.text = AddLeadingComboZeros(PlayerPrefs.GetInt(Constants.notesHit)) + "/" + AddLeadingComboZeros(noteCount);
        maxCombo.text = AddLeadingComboZeros(PlayerPrefs.GetInt(song + difficulty + Constants.maxCombo));
    }

    void SetHighScore()
    {
        if (PlayerPrefs.GetInt(Constants.score) > PlayerPrefs.GetInt(song + difficulty + Constants.highScore))
        {
            PlayerPrefs.SetInt(song + difficulty + Constants.highScore, PlayerPrefs.GetInt(Constants.score));
        }
    }

    void SetMaxCombo()
    {
        if (PlayerPrefs.GetInt(Constants.combo) > PlayerPrefs.GetInt(song + difficulty + Constants.maxCombo))
        {
            PlayerPrefs.SetInt(song + difficulty + Constants.maxCombo, PlayerPrefs.GetInt(Constants.combo));
        }
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
        scoreString = zeros + scoreString;
        return scoreString;
    }

    string AddLeadingComboZeros(int combo)
    {
        string comboString = combo.ToString();
        string zeros = "";

        int numZeros = Constants.comboDigits - comboString.Length;
        for (int i = 0; i < numZeros; i++)
        {
            zeros += "<color=#808080>0</color>"; // grey
        }
        comboString = zeros + comboString;
        return comboString;
    }

    void SetScoreRank()
    {
        Transform scoreRank = transform.Find("Content").Find("ScorePanel").Find("ScoreRank");
        string rank = PlayerPrefs.GetString(Constants.scoreRank);

        switch (rank)
        {
            case "SS":
                scoreRank.Find("RankSS").gameObject.SetActive(true);
                break;
            case "S":
                scoreRank.Find("RankS").gameObject.SetActive(true);
                break;
            case "A":
                scoreRank.Find("RankA").gameObject.SetActive(true);
                break;
            case "B":
                scoreRank.Find("RankB").gameObject.SetActive(true);
                break;
            case "C":
                scoreRank.Find("RankC").gameObject.SetActive(true);
                break;
            case "F":
                scoreRank.Find("RankF").gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
