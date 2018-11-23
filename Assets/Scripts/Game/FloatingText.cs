using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

    float destroyTime = 1.0f;

	void Start () {
        Destroy(gameObject, destroyTime);
    }
}
