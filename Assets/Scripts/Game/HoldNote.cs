using UnityEngine;

public class HoldNote : MonoBehaviour {

    GameObject tail;

    public void SetTail(GameObject tail)
    {
        this.tail = tail;
    }

    public void DestroyTail()
    {
        if (tail)
        {
            Destroy(tail);
        }
    }
}
