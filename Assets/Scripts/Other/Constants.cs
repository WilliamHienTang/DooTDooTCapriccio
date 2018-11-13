using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
    // Scenes
    public const string titleScreen = "TitleScreen";
    public const string mainMenu = "MainMenu";
    public const string songSelect = "SongSelect";
    public const string game = "Game";
    public const string scoreScreen = "ScoreScreen";

    // Audio
    public const string vivaceBGM = "vivace";
    public const string tapScreenSFX = "skill_slot_open";
    public const string kimiBGM = "kimi";
    public const string button1SFX = "bubble2";
    public const string button2SFX = "reward";
    public const string button3SFX = "bubble1";
    public const string kaiheitaiBGM = "kaiheitai";
    public const string perfectSFX = "perfect";
    public const string greatSFX = "great";
    public const string goodSFX = "good";
    public const string badSFX = "bad";
    public const string tapSFX = "tap";

    // Songs
    public const string soundscapePreview = "soundscape_preview";
    public const string soundscapeSong = "soundscape";
    public const string soundscapeTitle = "TRUE - Soundscape";
    public const string takarajimaPreview = "takarajima_preview";
    public const string takarajimaSong = "takarajima";
    public const string takarajimaTitle = "T-Square - Takarajima";
    public const string tuttiPreview = "tutti_preview";
    public const string tuttiSong = "tutti";
    public const string tuttiTitle = "ZAQ - Tutti!";

    // Difficulties
    public const string easy = "easy";
    public const string normal = "normal";
    public const string hard = "hard";
    public const string expert = "expert";

    // Color
    public static Color easyColor = new Color(0, 100f / 255f, 1);
    public static Color normalColor = new Color(0, 200f / 255f, 0);
    public static Color hardColor = new Color(1, 150f / 255f, 0);
    public static Color expertColor = new Color(1, 0, 0);
    public static Color blue = new Color(0, 150f/255f, 1);
    public static Color gold = new Color(1, 200f/255f, 0);

    // PlayerPrefs
    public const string noteSpeed = "NoteSpeed";
    public const string songVolume = "SongVolume";
    public const string gameSFXVolume = "GameSFXVolume";
    public const string BGMVolume = "BGMVolume";
    public const string SFXVolume = "SFXVolume";
    public const string selectedSong = "SelectedSong";
    public const string selectedSongTitle = "SelectedSongTitle";
    public const string difficulty = "Difficulty";
    public const string score = "Score";
    public const string combo = "Combo";
    public const string highScore = "HighScore";
    public const string maxCombo = "MaxCombo";
    public const string perfects = "Perfects";
    public const string greats = "Greats";
    public const string goods = "Goods";
    public const string bads = "Bads";
    public const string misses = "Misses";
    public const string notesHit = "NotesHit";

    // Tags
    public const string noteTag = "Note";
    public const string headNoteTag = "HeadNote";
    public const string holdLaneTag = "HoldLane";
    public const string tailNoteTag = "TailNote";
    public const string scoreType = "ScoreType";

    // Game variables
    public const int scoreDigits = 7;
    public const int comboDigits = 4;

    public const float activatorZ = 1.0f;
    public const float spawnZ = 25.0f;
    public const float followThroughZ = -10.0f;
    public const float songDelay = 5.0f;
    public const float maxNoteSpeed = 10.0f;
    public const float minNoteSpeed = 5.0f;

    public const float lane1X = -1.02f;
    public const float lane2X = -0.51f;
    public const float lane3X = 0f;
    public const float lane4X = 0.51f;
    public const float lane5X = 1.02f;

    public const string perfect = "perfect";
    public const string great = "great";
    public const string good = "good";
    public const string bad = "bad";

    public const int perfectScore = 1000;
    public const int greatScore = 750;
    public const int goodScore = 500;
    public const int badScore = 250;
}
