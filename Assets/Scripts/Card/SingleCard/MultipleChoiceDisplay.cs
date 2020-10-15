using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleChoiceDisplay : SingleCardDisplay
{
   // public Choice
    void Start()
    {
        currentCard = GameManager.Instance.currentCard;
        DisplayCard();
    }

    public override void DisplayCard()
    {
        // preparing camera for initial movement of card
        Camera.main.orthographic = false;
        startedInitialMovement = true;

        // preparing card data
        rightOptionText.text = ((Multiple)currentCard).choice1.rightText;
        leftOptionText.text = ((Multiple)currentCard).choice1.leftText;
        image.sprite = ((Multiple)currentCard).choice1.image;

    }
}
