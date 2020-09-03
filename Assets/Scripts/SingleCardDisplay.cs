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

    public delegate void DestroyCardEventHandler();
    public event DestroyCardEventHandler DestroyCardEvent;

    private Card currentCard;
    void Start()
    {
        currentCard = GameManager.Instance.currentCard;
        DisplayCard();
    }

    private void DisplayCard()
    {
        // put image
        // write options
        rightOptionText.text = ((Single)currentCard).choice.rightText;
        leftOptionText.text = ((Single)currentCard).choice.leftText;
        image.sprite = ((Single)currentCard).choice.image;
    }

    public void ShowOption(GameManager.Direction direction)
    {
        switch (direction)
        {
            case GameManager.Direction.RIGHT:
                rightOptionText.gameObject.SetActive(true);
                leftOptionText.gameObject.SetActive(false);
                break;
            case GameManager.Direction.LEFT:
                rightOptionText.gameObject.SetActive(false);
                leftOptionText.gameObject.SetActive(true);
                break;
            case GameManager.Direction.NONE:
                rightOptionText.gameObject.SetActive(false);
                leftOptionText.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void ChooseOption(GameManager.Direction direction)
    {
        switch (direction)
        {
            case GameManager.Direction.RIGHT:
                GameManager.Instance.currentCard = ((Single)currentCard).nextCardIfAnswerCorrect;
                break;
            case GameManager.Direction.LEFT:
                GameManager.Instance.currentCard = ((Single)currentCard).nextCardIfAnswerIncorrect;
                break;
            case GameManager.Direction.NONE:
                break;
            default:
                break;
        }

        DestroyCardEvent();
    }
}
