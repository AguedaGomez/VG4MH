using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceDisplay : MonoBehaviour
{
    private const string RIGHT_OPTION_NAME = "Right";
    private const string LEFT_OPTION_NAME = "Left";

    public CardManager cardManager;
    public List<Button> buttonOptionsList;
    public Button continueButton;

    private TextMeshProUGUI chosenOptionText;
    private Transform directionTransform;

    private Dictionary<Button, Card> linkedCardWithButton = new Dictionary<Button, Card>();
    private Button lastButtonSelected;

    private int countAnswers = 0;
    private List<bool> checkOptionList = new List<bool>(); //save false or true depending on the if the chosen options is correct or not
    public void DisplayOptions()
    {
        var currentCard = (Multiple)GameManager.Instance.currentCard;
        //first time that multiple options appear
        if (lastButtonSelected == null)
        {

            for (int i = 0; i < buttonOptionsList.Count; i++)
            {
                buttonOptionsList[i].interactable = true;
                TextMeshProUGUI buttonText = buttonOptionsList[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = TextToShowFormatter(currentCard.options[i].dialog);
                ChangeTextColor(buttonText, new Color(1f, 1f, 1f));
                linkedCardWithButton.Add(buttonOptionsList[i], currentCard.options[i]);
                buttonOptionsList[i].transform.Find(RIGHT_OPTION_NAME).gameObject.SetActive(false);
                buttonOptionsList[i].transform.Find(LEFT_OPTION_NAME).gameObject.SetActive(false);
            }
        }
        else
        {
            if (currentCard.type == Card.CardType.MULTIPLE)
            {
                lastButtonSelected.interactable = false;
                countAnswers++;

                if (cardManager.chosenDirection == Card.Direction.RIGHT)
                {
                    directionTransform = lastButtonSelected.transform.Find(RIGHT_OPTION_NAME);

                }
                else if (cardManager.chosenDirection == Card.Direction.LEFT)
                {
                    directionTransform = lastButtonSelected.transform.Find(LEFT_OPTION_NAME);
                    

                }
                chosenOptionText = directionTransform.GetComponentInChildren<TextMeshProUGUI>();
                WriteOptionText(chosenOptionText);
                ShowChosenOption(directionTransform, true);
                ChangeTextColor(lastButtonSelected.GetComponentInChildren<TextMeshProUGUI>(), new Color(0.5f, 0.5f, 0.5f));

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
                        ChangeTextColor(button.GetComponentInChildren<TextMeshProUGUI>(), new Color(0.5f, 0.5f, 0.5f));
                        if (button != lastButtonSelected)
                        {
                            var colors = button.colors;
                            colors.disabledColor = new Color(1, 1, 1, 0.2f);
                        }
                        else
                        {
                            //TODO REVISAR 4-12
                            chosenOptionText = lastButtonSelected.transform.Find(RIGHT_OPTION_NAME).GetComponent<TextMeshProUGUI>();
                            WriteOptionText(chosenOptionText);
                            ShowChosenOption(directionTransform, true);
                            ChangeTextColor(lastButtonSelected.GetComponentInChildren<TextMeshProUGUI>(), new Color(0.5f, 0.5f, 0.5f));
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

    private void ShowChosenOption(Transform textToShow, bool show)
    {
        textToShow.gameObject.SetActive(show);
    }

    private void WriteOptionText(TextMeshProUGUI textToShow)
    {
        textToShow.text = cardManager.lastOptionChosen;
    }

    private void ChangeTextColor(TextMeshProUGUI textToChange, Color color)
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
        directionTransform = null;

    }

    public void ChangeToNextCard()
    {
        if (checkOptionList.Contains(false))
            cardManager.UpdateCurrentCard(((Multiple)GameManager.Instance.currentCard).nextCardIfLeft);
        else
            cardManager.UpdateCurrentCard(((Multiple)GameManager.Instance.currentCard).nextCardIfRight);

        ResetVariables();
    }

    public void OpenCard(Button sender)
    {
        lastButtonSelected = sender;
        cardManager.UpdateCurrentCard(linkedCardWithButton[sender]);
    }

    
}
