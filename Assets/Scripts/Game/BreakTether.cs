using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTether : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (transform.parent.childCount < 3)
        {
            Destroy(transform.gameObject);
        }
	}
}
