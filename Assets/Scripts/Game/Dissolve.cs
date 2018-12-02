using UnityEngine;

public class Dissolve : MonoBehaviour {

    Material material;
    float noteSpeed;
    float length;
    float duration;
    float songTimer;
    float startTime;
    
    // Begin dissolve upon entering the dissolve collider
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "DissolveCollider")
        {
            startTime = (float)AudioSettings.dspTime;
            material.SetFloat("_DissolveAmount", Time.deltaTime / duration);
            enabled = true;
        }
    }

    void Start()
    {
        enabled = false;
        material = GetComponent<Renderer>().material;
        noteSpeed = PlayerPrefs.GetFloat(Constants.noteSpeed);
        length = GetComponent<Renderer>().bounds.size.z;
        duration = length / noteSpeed;
    }

    // Dissolve amount based on audio time and note speed
    void Update () {
        songTimer = (float)(AudioSettings.dspTime - startTime);
        material.SetFloat("_DissolveAmount", (songTimer + Time.deltaTime) / duration);
    }
}
