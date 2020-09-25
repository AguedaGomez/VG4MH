using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCardRewardManager : MonoBehaviour
{
    private const int MAXCUANTITY = 100;
    private const int DECREMENT = -10;
    private const int INCREMENT = 10;
    private Single currentCard;
    private RewardManager rewardManager;

    void Start()
    {
        rewardManager = transform.parent.GetComponent<RewardManager>();
    }

    public void CheckOptionChosen(Choice.Direction directionChosen)
    {
        GetCurrentCard();
        Card.Resource currentResource = currentCard.reward;
        int valueModifier = OptionIsCorrect(directionChosen) ? INCREMENT : DECREMENT;
        rewardManager.UpdateResource(currentResource, valueModifier);
       
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
