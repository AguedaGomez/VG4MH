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
    private float minLong = 1.25f;
    private Single.Direction currentDirection;
    private CardDisplay cardDisplay;
    private SingleCardRewardManager singleCardRewardManager;
    private Collider2D touchedCollider;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        directionChosen = false;
        currentDirection = Single.Direction.NONE;
        cardDisplay = GetComponent<CardDisplay>();
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
                    
                    //Check if something is touched
                    if (Physics2D.OverlapPoint(startPos) == null)
                        break;

                    offsetVector = transform.position - startPos;

                    //Check if the card is touched
                    touchedCollider = Physics2D.OverlapPoint(startPos);
                    touchingCard = touchedCollider.name == transform.name ? true : false;

                    break;

                case TouchPhase.Moved:
                    
                    if (touchingCard && !cardDisplay.startedInitialMovement) // if card is touched and the initial movement has finished
                    {
                        // Behaviour 
                        Vector3 currentTouchWorld = Camera.main.ScreenToWorldPoint(touchPosition);
                        transform.position = currentTouchWorld + offsetVector;
                        rotationAngle = currentTouchWorld.x * (-10f) / 2.5f;
                        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

                        direction = currentTouchWorld - startPos;
                        if (Mathf.Sign(direction.x) == 1) // show right option
                        {
                            currentDirection = Single.Direction.RIGHT;
                        }
                        else if (Mathf.Sign(direction.x) == -1) // show left option
                        {
                            currentDirection = Single.Direction.LEFT;
                        }

                        cardDisplay.ShowOption(currentDirection);
                    }

                    break;

                case TouchPhase.Ended:
                    if(touchingCard)
                    {
                        transform.position = initialPos;
                        transform.rotation = Quaternion.identity;

                        directionChosen = true;
                    }
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
                    currentDirection = Single.Direction.NONE;
                    cardDisplay.ShowOption(currentDirection);
                }
                directionChosen = false;
            }
            Debug.Log(currentDirection);
        }
    }

}
