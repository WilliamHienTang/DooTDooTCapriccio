using UnityEngine;

public class CanvasTap : MonoBehaviour {

    bool isTouchingDevice;
    public Transform canvasTapParticle;
    Camera mainCamera;

    void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
                isTouchingDevice = false;
                break;
            default:
                isTouchingDevice = true;
                break;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    // Instantiate canvas tap particle at touch position
    void Update ()
    {
        if (isTouchingDevice)
        {
            if(Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10));
                    Instantiate(canvasTapParticle, touchPosition, canvasTapParticle.rotation);
                }
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                Instantiate(canvasTapParticle, mousePosition, canvasTapParticle.rotation);
            }
        }
    }
}
