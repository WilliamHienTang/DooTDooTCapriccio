using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour {

    public TextMeshProUGUI fpsText;

    void Update()
    {
        float fps = (1.0f / Time.smoothDeltaTime);
        fpsText.text = "FPS: " + fps.ToString();
    }
}
