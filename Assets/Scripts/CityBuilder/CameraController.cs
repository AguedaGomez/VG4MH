using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 initialTouch;
    Vector3 offset;
    float zoomOutMin = 6;
    float zoomOutMax = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.touchCount > 0)
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                Zoom(difference * 0.01f);
            }
            else
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:

                        initialTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        offset = initialTouch - transform.position;
                        break;
                    case TouchPhase.Moved:
                        transform.position = initialTouch - offset;
                        offset = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - transform.position;

                        break;
                    default:
                        break;
                }
            }
        }
       
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
