using UnityEngine;

public class DefaultSettings : MonoBehaviour {

	void Start () {
        ResetPlayerPrefs();
        InitNoteCount();
	}

    // Reinit player prefs
    void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat(Constants.noteSpeed, 7.5f);
        PlayerPrefs.SetString(Constants.difficulty, Constants.easy);
        PlayerPrefs.SetString(Constants.selectedSong, Constants.soundscapeSong);
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
