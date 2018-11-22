using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTap : MonoBehaviour {

    bool isTouchingDevice;
    public Transform canvasTapParticle;

    void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                isTouchingDevice = false;
                break;
            case RuntimePlatform.Android:
                isTouchingDevice = true;
                break;
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isTouchingDevice)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10));
                Instantiate(canvasTapParticle, touchPosition, canvasTapParticle.rotation);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                Instantiate(canvasTapParticle, mousePosition, canvasTapParticle.rotation);
            }
        }
    }
}
