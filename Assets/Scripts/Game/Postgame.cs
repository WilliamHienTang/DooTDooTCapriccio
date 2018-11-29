using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Postgame : MonoBehaviour {

    // Game objects
    public Transform gameCanvas;
    public GameObject notePlatform;

    // Postgame objects
    public GameObject fireworksParticle;
    public GameObject fireworksText;

    public IEnumerator EndGame(float speedOffset)
    {
        // Disable pause button
        Pause pause = FindObjectOfType<Pause>();
        pause.SetPauseButtonActive(false);
        yield return new WaitForSeconds(speedOffset + 3.0f);

        // Set off firework particles
        StartCoroutine(SetOffFireworks(10));
        yield return new WaitForSeconds(3.0f);

        // Set score rank
        ScoreManager scoreManager = transform.GetComponent<ScoreManager>();
        scoreManager.SetScoreRank();

        // Load score screen scene
        SceneManager.LoadScene(Constants.scoreScreen);
    }

    IEnumerator SetOffFireworks(int numOfParticles)
    {
        // Disable the note platform
        notePlatform.SetActive(false);

        // Play fireworks audio
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(Constants.fireworks);

        // Instantiate big centered firework
        GameObject fireworks = Instantiate(fireworksParticle, new Vector3(fireworksParticle.transform.localPosition.x, fireworksParticle.transform.localPosition.y, fireworksParticle.transform.localPosition.z), fireworksParticle.transform.rotation);
        fireworks.transform.localScale = new Vector3(fireworksParticle.transform.localScale.x * 2, fireworksParticle.transform.localScale.x * 2, fireworksParticle.transform.localScale.x * 2);

        // Instantiate postgame text
        GameObject text = Instantiate(fireworksText, gameCanvas.position, Quaternion.identity, gameCanvas);
        if (PlayerPrefs.GetString(Constants.scoreRank) == "SS" || PlayerPrefs.GetString(Constants.scoreRank) == "S")
        {
            text.GetComponent<TextMeshProUGUI>().text = "FULL COMBO";
        }

        // Instantiate multiple firework particles
        float duration = fireworksParticle.GetComponent<ParticleSystem>().main.duration / 4.0f;
        for (int i = 0; i < numOfParticles; i++)
        {
            Instantiate(fireworksParticle, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 0.5f), fireworksParticle.transform.localPosition.z), fireworksParticle.transform.rotation);
            yield return new WaitForSeconds(duration);
        }
    }
}
