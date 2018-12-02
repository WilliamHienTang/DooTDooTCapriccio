using UnityEngine;

public class BreakTether : MonoBehaviour {

    GameObject tether;

    void Start()
    {
        tether = transform.Find("Tether").gameObject;
    }

    // Break if at least one sibling dies
    void Update () {
        if (transform.childCount < 3 || tether.transform.position.z <= Constants.activatorZ)
        {
            Destroy(tether);
            enabled = false;
        }
	}
}
