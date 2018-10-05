using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour {

    private ParticleSystem particleSystem;

    public void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (particleSystem)
        {
            if (!particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
