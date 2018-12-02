using UnityEngine;

public class BreakTether : MonoBehaviour {

    GameObject tether;

    void Awake()
    {
        tether = transform.Find("Tether").gameObject;
    }

    void OnEnable()
    {
        tether.SetActive(true);
    }

    // Break if at least one sibling dies
    void Update () {
        if (GetComponentsInChildren<Transform>().GetLength(0) < 3 || tether.transform.position.z <= Constants.activatorZ)
        {
            tether.SetActive(false);
        }
	}
}
