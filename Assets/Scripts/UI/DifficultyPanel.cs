using UnityEngine;

public class DifficultyPanel : MonoBehaviour {

	void Start () {
        string difficulty = PlayerPrefs.GetString(Constants.difficulty);

        switch (difficulty)
        {
            case Constants.easy:
                transform.GetComponent<PanelHandler>().PanelAnim(0);
                break;
            case Constants.normal:
                transform.GetComponent<PanelHandler>().PanelAnim(1);
                break;
            case Constants.hard:
                transform.GetComponent<PanelHandler>().PanelAnim(2);
                break;
            case Constants.expert:
                transform.GetComponent<PanelHandler>().PanelAnim(3);
                break;
            default:
                break;
        }

        transform.GetComponent<AnimatePanel>().PlayAnimator();
    }

    public void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                PlayerPrefs.SetString(Constants.difficulty, Constants.easy);
                break;
            case 2:
                PlayerPrefs.SetString(Constants.difficulty, Constants.normal);
                break;
            case 3:
                PlayerPrefs.SetString(Constants.difficulty, Constants.hard);
                break;
            case 4:
                PlayerPrefs.SetString(Constants.difficulty, Constants.expert);
                break;
            default:
                break;
        }
    }
}
