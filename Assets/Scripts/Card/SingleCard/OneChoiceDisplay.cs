﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneChoiceDisplay : SingleCardDisplay
{
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
        rightOptionText.text = ((Single)currentCard).choice.rightText;
        leftOptionText.text = ((Single)currentCard).choice.leftText;
        image.sprite = ((Single)currentCard).choice.image;

    }

}
