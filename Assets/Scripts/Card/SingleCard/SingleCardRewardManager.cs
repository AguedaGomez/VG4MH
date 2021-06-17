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
        rewardManager = transform.parent.GetComponent<RewardManager>();
        cardManager = transform.parent.GetComponent<CardManager>();
    }

    public void CheckOptionChosen(Card.Direction directionChosen)
    {
        GetCurrentCard();
        Card.Resource currentResource = currentCard.reward;

        int valueModifier = OptionIsCorrect(directionChosen) ? INCREMENT : DECREMENT;
        Card nextCard = directionChosen == Card.Direction.RIGHT ? currentCard.nextCardIfRight : currentCard.nextCardIfLeft;
        
        SaveOptionChosen(directionChosen); //action for multipleOptions

        rewardManager.UpdateResource(currentResource, valueModifier);

        if (nextCard.characterName != currentCard.characterName)
            cardManager.LoadScene();
        else
            cardManager.UpdateCurrentCard(nextCard);
       
    }

    private void  GetCurrentCard()
    {
        currentCard = (Single)GameManager.Instance.currentCard;
    }

    private bool OptionIsCorrect(Card.Direction directionChosen)
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
