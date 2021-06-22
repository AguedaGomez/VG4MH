using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceDisplay : MonoBehaviour
{
    private const string RIGHT_OPTION_NAME = "Right Chosen Option";
    private const string LEFT_OPTION_NAME = "Left Chosen Option";

    public CardManager cardManager;
    public List<Button> buttonOptionsList;
    public Button continueButton;

    private Text chosenOptionText;

    private Dictionary<Button, Card> linkedCardWithButton = new Dictionary<Button, Card>();
    private Button lastButtonSelected;

    private int countAnswers = 0;
    private List<bool> checkOptionList = new List<bool>(); //save false or true depending on the if the chosen options is correct or not
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
                countAnswers++;

                if (cardManager.chosenDirection == Card.Direction.RIGHT)
                    chosenOptionText = lastButtonSelected.transform.Find(RIGHT_OPTION_NAME).GetComponent<Text>();
                else if (cardManager.chosenDirection == Card.Direction.LEFT)
                    chosenOptionText = lastButtonSelected.transform.Find(LEFT_OPTION_NAME).GetComponent<Text>();

                WriteOptionText(chosenOptionText);
                ShowChosenOption(chosenOptionText, true);
                ChangeTextColor(lastButtonSelected.GetComponentInChildren<Text>(), new Color(0.5f,0.5f,0.5f));

                checkOptionList.Add(cardManager.islastOptionCorrect);

                if (linkedCardWithButton.Count == countAnswers)
                {
                    continueButton.gameObject.SetActive(true);
                    cardManager.ShowCharacterName(false);
                    
                }
            }
            else if (currentCard.type == Card.CardType.MULTIPLE_ONE)
            {
                checkOptionList.Add(cardManager.islastOptionCorrect);
                if (cardManager.chosenDirection == Card.Direction.RIGHT)
                {
                    foreach (Button button in buttonOptionsList)
                    {
                        button.interactable = false;
                        ChangeTextColor(button.GetComponentInChildren<Text>(), new Color(0.5f, 0.5f, 0.5f));
                        if (button != lastButtonSelected)
                        {
                            var colors = button.colors;
                            colors.disabledColor = new Color(1,1,1,0.2f);
                        }
                        else
                        {
                            chosenOptionText = lastButtonSelected.transform.Find(RIGHT_OPTION_NAME).GetComponent<Text>();
                            WriteOptionText(chosenOptionText);
                            ShowChosenOption(chosenOptionText, true);
                            ChangeTextColor(lastButtonSelected.GetComponentInChildren<Text>(), new Color(0.5f, 0.5f, 0.5f));
                            continueButton.gameObject.SetActive(true);
                            cardManager.ShowCharacterName(false);

                        }
                    }
                }
                
            }
        }

    }

    private string TextToShowFormatter(string textToFormat)
    {
        return textToFormat.Length < 30 ? textToFormat : textToFormat.Substring(0, 30) + "...";
    }

    private void ShowChosenOption(Text textToShow, bool show)
    {
        textToShow.gameObject.SetActive(show); 
    }

    private void WriteOptionText(Text textToShow)
    {
        textToShow.text = cardManager.lastOptionChosen;
    }

    private void ChangeTextColor(Text textToChange, Color color)
    {
        textToChange.color = color;
    }
    private void ResetVariables()
    {
        lastButtonSelected = null;
        countAnswers = 0;
        linkedCardWithButton.Clear();
        checkOptionList.Clear();
        continueButton.gameObject.SetActive(false);

    }

    public void ChangeToNextCard()
    {
        if (checkOptionList.Contains(false))
            cardManager.UpdateCurrentCard(((Multiple)GameManager.Instance.currentCard).nextCardIfAnyAnswerIsWrong);
        else
            cardManager.UpdateCurrentCard(((Multiple)GameManager.Instance.currentCard).nextCardIfAllAnswersAreCorrect);

        ResetVariables();
    }

    public void OpenCard(Button sender)
    {
        lastButtonSelected = sender;
        cardManager.UpdateCurrentCard(linkedCardWithButton[sender]);
    }

}
