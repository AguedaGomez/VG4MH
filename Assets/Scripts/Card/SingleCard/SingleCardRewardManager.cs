using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleCardRewardManager : MonoBehaviour
{
    private const int DECREMENT = -10;
    private const int INCREMENT = 10;
    private Single currentCard;
    private RewardManager rewardManager;
    private CardManager cardManager;


    void Start()
    {
        rewardManager = (RewardManager)FindObjectOfType(typeof(RewardManager));
        cardManager = (CardManager)FindObjectOfType(typeof(CardManager));
    }

    public void CheckOptionChosen(Single.Direction directionChosen)
    {
        GetCurrentCard();
        Card.Resource currentResource = currentCard.reward;

        int valueModifier = OptionIsCorrect(directionChosen) ? INCREMENT : DECREMENT;
        Card nextCard = directionChosen == Single.Direction.RIGHT ? currentCard.nextCardIfRight : currentCard.nextCardIfLeft; // change if more directions are added

        //rewardManager.UpdateResource(currentResource, valueModifier);
        SaveOptionChosen(directionChosen); //action for multipleOptions

        if (directionChosen != Card.Direction.NONE)
            rewardManager.UpdateResource(currentResource, valueModifier);

        if (nextCard.characterName != currentCard.characterName)
        {
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
        return currentCard.correctDirection == directionChosen;
      
    }

    private void SaveOptionChosen(Card.Direction directionChosen)
    {
        cardManager.chosenDirection = directionChosen;
        cardManager.lastOptionChosen = directionChosen == Card.Direction.RIGHT ? currentCard.rightText : currentCard.leftText;
        cardManager.islastOptionCorrect = OptionIsCorrect(directionChosen);
    }
}
