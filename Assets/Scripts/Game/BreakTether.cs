using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTether : MonoBehaviour {

	// Break if at least one sibling dies
	void Update () {
        if (transform.parent.childCount < 3)
        {
            Destroy(transform.gameObject);
        }
	}
}
