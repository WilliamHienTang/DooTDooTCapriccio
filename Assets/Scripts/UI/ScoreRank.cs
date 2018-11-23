using UnityEngine;

public class ScoreRank : MonoBehaviour {

    GameObject RankSS;
    GameObject RankS;
    GameObject RankA;
    GameObject RankB;
    GameObject RankC;
    GameObject RankF;

    void Start () {
        RankSS = transform.Find("RankSS").gameObject;
        RankS = transform.Find("RankS").gameObject;
        RankA = transform.Find("RankA").gameObject;
        RankB = transform.Find("RankB").gameObject;
        RankC = transform.Find("RankC").gameObject;
        RankF = transform.Find("RankF").gameObject;
    }

    // Display the corresponding obtained rank of the selected song at the selected difficulty
    void Update()
    {
        string song = PlayerPrefs.GetString(Constants.selectedSong);
        string difficulty = PlayerPrefs.GetString(Constants.difficulty);
        string rank = PlayerPrefs.GetString(song + difficulty + Constants.highRank);

        switch (rank)
        {
            case "SS":
                RankSS.SetActive(true);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
            case "S":
                RankSS.SetActive(false);
                RankS.SetActive(true);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
            case "A":
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(true);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
            case "B":
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(true);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
            case "C":
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(true);
                RankF.SetActive(false);
                break;
            case "F":
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(true);
                break;
            default:
                RankSS.SetActive(false);
                RankS.SetActive(false);
                RankA.SetActive(false);
                RankB.SetActive(false);
                RankC.SetActive(false);
                RankF.SetActive(false);
                break;
        }
    }
}
