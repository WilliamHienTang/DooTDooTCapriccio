using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

    readonly float deactivateTime = 1.0f;

	void OnEnable () {
        StartCoroutine(SetInactiveCoroutine());
    }

    IEnumerator SetInactiveCoroutine()
    {
        yield return new WaitForSeconds(deactivateTime);
        gameObject.SetActive(false);
    }
}
