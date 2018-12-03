using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFPS : MonoBehaviour {

    public int fps = 60;

	void Start () {
        Application.targetFrameRate = fps;
	}
}
