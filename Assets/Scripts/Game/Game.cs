using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public GameObject HitCollider1;
    public GameObject HitCollider2;
    public GameObject HitCollider3;
    public GameObject HitCollider4;

    public void PauseButton()
    {
        enabled = false;
        SceneManager.LoadScene("SongSelect");
    }

    public void ButtonAudio()
    {
        FindObjectOfType<AudioManager>().Play("bubble2");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "HitCollider1")
                {
                    HitCollider1.GetComponent<HitCollider>().OnPress();
                }
                else if (hit.transform.name == "HitCollider2")
                {
                    HitCollider2.GetComponent<HitCollider>().OnPress();
                }
                else if (hit.transform.name == "HitCollider3")
                {
                    HitCollider3.GetComponent<HitCollider>().OnPress();
                }
                else if (hit.transform.name == "HitCollider4")
                {
                    HitCollider4.GetComponent<HitCollider>().OnPress();
                }
            }
        
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "HitCollider1")
                {
                    HitCollider1.GetComponent<HitCollider>().OnPress();
                }
                else if (hit.transform.name == "HitCollider2")
                {
                    HitCollider2.GetComponent<HitCollider>().OnPress();
                }
                else if (hit.transform.name == "HitCollider3")
                {
                    HitCollider3.GetComponent<HitCollider>().OnPress();
                }
                else if (hit.transform.name == "HitCollider4")
                {
                    HitCollider4.GetComponent<HitCollider>().OnPress();
                }
            }
        }
    }
}
