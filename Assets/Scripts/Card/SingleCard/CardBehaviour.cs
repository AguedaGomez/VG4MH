using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    
    private Vector3 startPos;
    private Vector3 initialPos;
    private Vector3 direction;
    private Vector3 offsetVector;
    private Vector3 touchPosition;
    private bool directionChosen;
    private bool touchingCard;
    private float rotationAngle;
    private float minLong = 2.0f;
    private Choice.Direction currentDirection;
    private SingleCardDisplay singleCardDisplay;
    private SingleCardRewardManager singleCardRewardManager;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        directionChosen = false;
        currentDirection = Choice.Direction.NONE;
        singleCardDisplay = GetComponent<SingleCardDisplay>();
        singleCardRewardManager = GetComponent<SingleCardRewardManager>();
    }


    void LateUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = new Vector3(touch.position.x, touch.position.y, 0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = Camera.main.ScreenToWorldPoint(touchPosition);
                    offsetVector = transform.position - startPos;

                    //Check if card is touched
                    touchingCard = Physics2D.OverlapPoint(startPos); 

                    break;

                case TouchPhase.Moved:
                    
                    if (touchingCard)
                    {
                        // Behaviour 
                        Vector3 currentTouchWorld = Camera.main.ScreenToWorldPoint(touchPosition);
                        transform.position = currentTouchWorld + offsetVector;
                        rotationAngle = currentTouchWorld.x * (-10f) / 2.5f;
                        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

                        direction = currentTouchWorld - startPos;
                        if (Mathf.Sign(direction.x) == 1) // show right option
                        {
                            currentDirection = Choice.Direction.RIGHT;

                        }
                        else if (Mathf.Sign(direction.x) == -1) // show left option
                        {
                            currentDirection = Choice.Direction.LEFT;
                        }
                        singleCardDisplay.ShowOption(currentDirection);
                    }

                    break;

                case TouchPhase.Ended:
                    transform.position = initialPos;
                    transform.rotation = Quaternion.identity;

                    directionChosen = true;
                    break;

                default:
                    break;
            }

            if (directionChosen)
            {
                if (Mathf.Abs(direction.x) > minLong)
                {
                    singleCardRewardManager.CheckOptionChosen(currentDirection);
                }
                else
                {
                    currentDirection = Choice.Direction.NONE;
                    singleCardDisplay.ShowOption(currentDirection);
                }
                directionChosen = false;
            }
        }
    }

}
