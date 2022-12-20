using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleCardRewardManager : MonoBehaviour
{
    private const int DECREMENT = 0;
    private const int INCREMENT = 1;
    private Single currentCard;
    private RewardManager rewardManager;
    private CardManager cardManager;
    private TOP_Hud_Controller hudManager;

    void Start()
    {
        rewardManager = (RewardManager)FindObjectOfType(typeof(RewardManager));
        cardManager = (CardManager)FindObjectOfType(typeof(CardManager));
        hudManager = (TOP_Hud_Controller)FindObjectOfType(typeof(TOP_Hud_Controller));
    }

    public void CheckOptionChosen(Single.Direction directionChosen)
    {
        GetCurrentCard();
        Card.Resource currentResource = currentCard.reward;

        int valueModifier = OptionIsCorrect(directionChosen) ? INCREMENT : DECREMENT;
        Card nextCard = directionChosen == Single.Direction.RIGHT ? currentCard.nextCardIfRight : currentCard.nextCardIfLeft; // change if more directions are added

        SaveOptionChosen(directionChosen); //action for multipleOptions
        
        if (directionChosen != Card.Direction.NONE)
            rewardManager.UpdateResource(currentResource, valueModifier);

        //Realizar Animación según el reward que varie
        hudManager.Start_VisualResourceStatChange(currentResource, valueModifier);
        

        //Contemplar caso de que no haya siguiente currentcard en el dialogo
        if (nextCard.characterName != currentCard.characterName && nextCard.characterName != "Thomas Gruber")
        {
            Card.Resource checkpoint = currentResource;
            if (!GameManager.Instance.checkPointsStory.Contains(checkpoint)) GameManager.Instance.checkPointsStory.Add(checkpoint);
            GameManager.Instance.currentCard = nextCard;
            cardManager.LoadScene();
        }
        else
            cardManager.UpdateCurrentCard(nextCard);
       
    }

    private void  GetCurrentCard()
    {
        currentCard = (Single)GameManager.Instance.currentCard;
    }

    private bool OptionIsCorrect(Single.Direction directionChosen)
    {
        if (currentCard.correctDirection == Card.Direction.BOTH)
            return true;
        return currentCard.correctDirection == directionChosen;
      
    }

    private void SaveOptionChosen(Card.Direction directionChosen)
    {
        cardManager.chosenDirection = directionChosen;
        cardManager.lastOptionChosen = directionChosen == Card.Direction.RIGHT ? currentCard.rightText : currentCard.leftText;
        cardManager.islastOptionCorrect = OptionIsCorrect(directionChosen);
    }
}
