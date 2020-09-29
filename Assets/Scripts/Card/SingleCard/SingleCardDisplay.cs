using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleCardDisplay : MonoBehaviour
{
    public TextMeshPro rightOptionText;
    public TextMeshPro leftOptionText;
    public SpriteRenderer image;
    public bool startedInitialMovement = false;

    private Card currentCard;
    private float rotationSpeed = 150f;

    //fov perspective 53.2

    void Start()
    {
        currentCard = GameManager.Instance.currentCard;
        DisplayCard();
    }

    private void DisplayCard()
    {
        // preparing camera for initial movement of card
        Camera.main.orthographic = false;
        startedInitialMovement = true;

        // preparing card data
        rightOptionText.text = ((Single)currentCard).choice.rightText;
        leftOptionText.text = ((Single)currentCard).choice.leftText;
        image.sprite = ((Single)currentCard).choice.image;
    }

    public void ShowOption(Choice.Direction direction)
    {
        switch (direction)
        {
            case Choice.Direction.RIGHT:
                rightOptionText.gameObject.SetActive(true);
                leftOptionText.gameObject.SetActive(false);
                break;
            case Choice.Direction.LEFT:
                rightOptionText.gameObject.SetActive(false);
                leftOptionText.gameObject.SetActive(true);
                break;
            case Choice.Direction.NONE:
                rightOptionText.gameObject.SetActive(false);
                leftOptionText.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (startedInitialMovement)
        {
            if (transform.rotation.y > 0)
            {
                transform.RotateAround(transform.position, -transform.up, Time.deltaTime * rotationSpeed);
            }
            else
            {
                Camera.main.orthographic = true;
                startedInitialMovement = false;
            }
        }
    }

}
