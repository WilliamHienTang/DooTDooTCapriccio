using UnityEngine;

public class HoldNote : MonoBehaviour {

    GameObject tail;

    public void SetTail(GameObject tail)
    {
        this.tail = tail;
    }

    void OnEnable()
    {
        if(tail != null){
            tail.SetActive(true);
        }
    }

    public void DeactivateTail()
    {
        if (tail)
        {
            tail.SetActive(false);
        }
    }
}
