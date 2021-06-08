using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceDisplay : MonoBehaviour
{
    //public GameObject MultipleChoicePanel;
    public CardManager cardManager;
    public List<Button> buttonOptionsList;

    private Dictionary<Button, Card> linkedCardWithButton = new Dictionary<Button, Card>();
    private Button lastButtonSelected;
    public void DisplayOptions()
    {
        var currentCard = (Multiple)GameManager.Instance.currentCard;
        //first time that multiple options appear
        if (lastButtonSelected == null) {

            for (int i = 0; i < buttonOptionsList.Count; i++)
            {
                buttonOptionsList[i].interactable = true;
                buttonOptionsList[i].GetComponentInChildren<Text>().text = TextToShowFormatter(currentCard.options[i].dialog);
                linkedCardWithButton.Add(buttonOptionsList[i], currentCard.options[i]);
            }
        } else
        {
            if (currentCard.type == Card.CardType.MULTIPLE)
            {
                lastButtonSelected.interactable = false;
                lastButtonSelected.GetComponentInChildren<Text>().text = cardManager.lastOptionChosen;
                //linkedCardWithButton.Remove(lastButtonSelected);
                if (linkedCardWithButton.Count == 0)
                {
                    lastButtonSelected = null;
                    cardManager.UpdateCurrentCard(((Multiple)GameManager.Instance.currentCard).nextCard);
                }
            }
        }

    }

    private string TextToShowFormatter(string textToFormat)
    {
        return textToFormat.Length < 30 ? textToFormat : textToFormat.Substring(0, 30) + "...";
    }

    public void OpenCard(Button sender)
    {
        lastButtonSelected = sender;
        cardManager.UpdateCurrentCard(linkedCardWithButton[sender]);
    }

}
