using UnityEngine;

public class FloatingText : MonoBehaviour {

    readonly float destroyTime = 1.0f;

	void Start () {
        Destroy(gameObject, destroyTime);
    }
}
