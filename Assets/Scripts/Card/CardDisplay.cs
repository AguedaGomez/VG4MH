using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CardDisplay : MonoBehaviour
{
    public TextMeshPro rightOptionText;
    public TextMeshPro leftOptionText;
    public SpriteRenderer image;
    public bool startedInitialMovement = false;

    public Card currentCard;
    private float rotationSpeed = 150f;


    //fov perspective 53.2

    protected virtual void Start()
    {
        currentCard = GameManager.Instance.currentCard;
        DisplayCard();
    }

    public virtual void DisplayCard()
    {
    }

    public virtual void ShowOption(Card.Direction direction)
    {
        
        switch (direction)
        {
            case Card.Direction.RIGHT:
                rightOptionText.gameObject.SetActive(true);
                leftOptionText.gameObject.SetActive(false);
                break;
            case Card.Direction.LEFT:
                rightOptionText.gameObject.SetActive(false);
                leftOptionText.gameObject.SetActive(true);
                break;
            case Card.Direction.NONE:
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

public interface IDisplay
{
    void DisplayCard();
}
