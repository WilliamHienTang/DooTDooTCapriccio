using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawn : MonoBehaviour {
    public float spawnTime;
    public float tailSpawnTime;
    public int laneIndex;

    public float length;

    // Use this for initialization
    void Awake()
    {
        if (tailSpawnTime > 0)
        {
            length = PlayerPrefs.GetFloat("velocty") * (tailSpawnTime - spawnTime);
        }
        else
        {
            length = 0;
        }
    }
}
