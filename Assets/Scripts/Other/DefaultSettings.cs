using UnityEngine;

public class DefaultSettings : MonoBehaviour {

	void Start () {
        FirstLoad();
        ResetPlayerPrefs(); // Right now I want to reset every time
    }

    // Only occurs for the first time the player loads the game
    void FirstLoad(){
        if (!PlayerPrefs.HasKey(Constants.firstLoad)){
            ResetPlayerPrefs();
        }
    }

    // Reinit player prefs
    void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat(Constants.noteSpeed, 7.5f);
        PlayerPrefs.SetString(Constants.difficulty, Constants.easy);
        PlayerPrefs.SetString(Constants.selectedSong, Constants.soundscapeSong);
        PlayerPrefs.SetInt(Constants.firstLoad, 1);
        InitNoteCount();
    }

    void InitNoteCount()
    {
        PlayerPrefs.SetInt("soundscapeEasyNoteCount", Constants.soundscapeEasyNoteCount);
        PlayerPrefs.SetInt("soundscapeNormalNoteCount", Constants.soundscapeNormalNoteCount);
        PlayerPrefs.SetInt("soundscapeHardNoteCount", Constants.soundscapeHardNoteCount);
        PlayerPrefs.SetInt("soundscapeExpertNoteCount", Constants.soundscapeExpertNoteCount);
        PlayerPrefs.SetInt("takarajimaEasyNoteCount", Constants.takarajimaEasyNoteCount);
        PlayerPrefs.SetInt("takarajimaNormalNoteCount", Constants.takarajimaNormalNoteCount);
        PlayerPrefs.SetInt("takarajimaHardNoteCount", Constants.takarajimaHardNoteCount);
        PlayerPrefs.SetInt("takarajimaExpertNoteCount", Constants.takarajimaExpertNoteCount);
        PlayerPrefs.SetInt("tuttiEasyNoteCount", Constants.tuttiEasyNoteCount);
        PlayerPrefs.SetInt("tuttiNormalNoteCount", Constants.tuttiNormalNoteCount);
        PlayerPrefs.SetInt("tuttiHardNoteCount", Constants.tuttiHardNoteCount);
        PlayerPrefs.SetInt("tuttiExpertNoteCount", Constants.tuttiExpertNoteCount);
    }
}
