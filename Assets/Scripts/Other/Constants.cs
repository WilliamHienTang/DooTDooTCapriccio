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
    public const string soundscapePreview = "soundscape_preview";
    public const string soundscapeSong = "soundscape";
    public const string takarajimaPreview = "takarajima_preview";
    public const string takarajimaSong = "takarajima";
    public const string tuttiPreview = "tutti_preview";
    public const string tuttiSong = "tutti";

    // PlayerPrefs
    public const string noteSpeed = "NoteSpeed";
    public const string songVolume = "SongVolume";
    public const string gameSFXVolume = "GameSFXVolume";
    public const string BGMVolume = "BGMVolume";
    public const string SFXVolume = "SFXVolume";
    public const string selectedSong = "SelectedSong";
    public const string difficulty = "Difficulty";
    public const string score = "Score";
    public const string combo = "Combo";

    // Tags
    public const string noteTag = "Note";
    public const string headNoteTag = "HeadNote";
    public const string holdLaneTag = "HoldLane";
    public const string tailNoteTag = "TailNote";
    public const string scoreType = "ScoreType";

    // Game variables
    public const float activatorZ = 1.0f;
    public const float spawnZ = 20.0f;
    public const float songDelay = 5.0f;

    public const float lane1X = -1.02f;
    public const float lane2X = -0.51f;
    public const float lane3X = 0f;
    public const float lane4X = 0.51f;
    public const float lane5X = 1.02f;

    public const int perfectScore = 1000;
    public const int greatScore = 750;
    public const int goodScore = 500;
    public const int badScore = 250;
}
