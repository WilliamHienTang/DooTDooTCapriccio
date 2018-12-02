using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

    readonly float destroyTime = 1.0f;

	void OnEnable () {
        StartCoroutine(SetInactiveCoroutine());
    }

    IEnumerator SetInactiveCoroutine()
    {
        yield return new WaitForSeconds(destroyTime);
        gameObject.SetActive(false);
    }
}
