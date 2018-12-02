using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public ParticleSystem prefab;
        public int size;
    }

    public Dictionary<string, Queue<ParticleSystem>> poolDictionary;
    public List<Pool> pools;

    #region simpleton
    public static ParticleSystemPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<ParticleSystem>>();

        foreach (Pool pool in pools)
        {
            Queue<ParticleSystem> objectPool = new Queue<ParticleSystem>();

            for (int i = 0; i < pool.size; i++)
            {
                ParticleSystem ps = Instantiate(pool.prefab);
                objectPool.Enqueue(ps);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Use the same way as Instantiate, then play
    public ParticleSystem PlayParticle(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        ParticleSystem spawnObject = poolDictionary[tag].Dequeue();

        // requeue
        poolDictionary[tag].Enqueue(spawnObject);

        // set active, position, and rotation
        spawnObject.transform.position = position;
        spawnObject.transform.rotation = rotation;
        spawnObject.Play();

        return spawnObject;
    }

    public ParticleSystem PlayParticle(string tag, Vector3 position, Quaternion rotation, bool loop)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        ParticleSystem spawnObject = poolDictionary[tag].Dequeue();

        // requeue
        poolDictionary[tag].Enqueue(spawnObject);

        // set active, position, and rotation
        spawnObject.transform.position = position;
        spawnObject.transform.rotation = rotation;

        // Set particle system and its children to loop
        if(loop){
            spawnObject.loop = true;

            for (int i = 0; i < spawnObject.transform.childCount; i++)
            {
                spawnObject.transform.GetChild(i).GetComponent<ParticleSystem>().loop = true;
            }
        }
        spawnObject.Play();


        return spawnObject;
    }
}
