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

    public void CheckOptionChosen(Choice.Direction directionChosen)
    {
        GetCurrentCard();
        Card.Resource currentResource = currentCard.reward;

        int valueModifier = OptionIsCorrect(directionChosen) ? INCREMENT : DECREMENT;
        Card nextCard = directionChosen == Choice.Direction.RIGHT ? currentCard.nextCardIfRight : currentCard.nextCardIfLeft; // change if more directions are added

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

    private bool OptionIsCorrect(Choice.Direction directionChosen)
    {
        return currentCard.choice.correctDirection == directionChosen;
      
    }
}
