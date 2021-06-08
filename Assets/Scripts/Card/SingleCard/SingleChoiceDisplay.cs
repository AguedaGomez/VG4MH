using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleChoiceDisplay : CardDisplay, IDisplay
{
    // Start is called before the first frame update
    public override void DisplayCard()
    {
        // preparing camera for initial movement of card
        Camera.main.orthographic = false;
        startedInitialMovement = true;

        // preparing card data
        rightOptionText.text = ((Single)currentCard).rightText;
        leftOptionText.text = ((Single)currentCard).leftText;
        image.sprite = ((Single)currentCard).image;
    }



}
