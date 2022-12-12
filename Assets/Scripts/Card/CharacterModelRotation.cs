using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelRotation : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private Quaternion initialRotation;
    private float rotateSpeedModifier = 0.09f;

    void Start()
    {
        initialRotation = transform.rotation;
    }
    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, touch.deltaPosition.x * rotateSpeedModifier, 0f);
                transform.rotation = rotationY * transform.rotation;
                //Debug.Log(transform.localEulerAngles);
                //rotationY = Quaternion.Euler(0f, Mathf.Clamp(-touch.deltaPosition.x * rotateSpeedModifier, 0f, 180f), 0f);

            }

            else if(touch.phase == TouchPhase.Ended)
            {
                transform.rotation = initialRotation;
            }
        }
    }
}