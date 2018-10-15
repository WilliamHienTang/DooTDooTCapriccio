using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private bool _isTouchingDevice;

    public GameObject HitCollider1;
    public GameObject HitCollider2;
    public GameObject HitCollider3;
    public GameObject HitCollider4;
    public GameObject HitCollider5;

    void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                _isTouchingDevice = false;
                break;
            case RuntimePlatform.Android:
                _isTouchingDevice = true;
                break;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (FindObjectOfType<Pause>().IsPaused())
        {
            return;
        }

        if (_isTouchingDevice)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.touches[i];
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && touch.phase == TouchPhase.Began)
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
                    else if (hit.transform.name == "HitCollider5")
                    {
                        HitCollider5.GetComponent<HitCollider>().OnPress();
                    }
                }

                if (Physics.Raycast(ray, out hit) && touch.phase == TouchPhase.Ended)
                {
                    if (hit.transform.name == "HitCollider1")
                    {
                        HitCollider1.GetComponent<HitCollider>().OnRelease();
                    }
                    else if (hit.transform.name == "HitCollider2")
                    {
                        HitCollider2.GetComponent<HitCollider>().OnRelease();
                    }
                    else if (hit.transform.name == "HitCollider3")
                    {
                        HitCollider3.GetComponent<HitCollider>().OnRelease();
                    }
                    else if (hit.transform.name == "HitCollider4")
                    {
                        HitCollider4.GetComponent<HitCollider>().OnRelease();
                    }
                    else if (hit.transform.name == "HitCollider5")
                    {
                        HitCollider5.GetComponent<HitCollider>().OnPress();
                    }
                }
            }
        }

        else
        {
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
                    else if (hit.transform.name == "HitCollider5")
                    {
                        HitCollider5.GetComponent<HitCollider>().OnPress();
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "HitCollider1")
                    {
                        HitCollider1.GetComponent<HitCollider>().OnRelease();
                    }
                    else if (hit.transform.name == "HitCollider2")
                    {
                        HitCollider2.GetComponent<HitCollider>().OnRelease();
                    }
                    else if (hit.transform.name == "HitCollider3")
                    {
                        HitCollider3.GetComponent<HitCollider>().OnRelease();
                    }
                    else if (hit.transform.name == "HitCollider4")
                    {
                        HitCollider4.GetComponent<HitCollider>().OnRelease();
                    }
                    else if (hit.transform.name == "HitCollider5")
                    {
                        HitCollider5.GetComponent<HitCollider>().OnPress();
                    }
                }
            }
        }
    }
}
