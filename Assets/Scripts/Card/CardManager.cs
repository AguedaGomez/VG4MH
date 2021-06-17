using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{

    public GameObject cardPrefab;
    public GameObject multipleChoicePanel;
    public GameObject backCard;

    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI characterNameText;

    private GameObject cardGameObject;
    public MultipleChoiceDisplay multipleChoiceDisplay;

    public string lastOptionChosen;
    public bool islastOptionCorrect;
    public Card.Direction chosenDirection;

    // Start is called before the first frame update
    void Start()
    {
        CommonDisplay();
        CreateCard();
    }

    // Show dialog text and character name
    private void CommonDisplay()
    {
        Card currentCard = GameManager.Instance.currentCard;
        dialogText.text = currentCard.dialog;
        characterNameText.text = currentCard.characterName;
        ShowCharacterName(true);
    }
    public void CreateCard()
    {
        if (GameManager.Instance.currentCard.type == Card.CardType.SINGLE)
        {
            backCard.SetActive(true);
            multipleChoicePanel.SetActive(false);
            cardGameObject = Instantiate(cardPrefab, transform);
        }

        else if (GameManager.Instance.currentCard.type == Card.CardType.MULTIPLE || GameManager.Instance.currentCard.type == Card.CardType.MULTIPLE_ONE)
        {
            backCard.SetActive(false);
            multipleChoicePanel.SetActive(true);
            multipleChoiceDisplay.DisplayOptions();
       
        }
            
    }

    public void UpdateCurrentCard(Card nextCard)
    {

        GameManager.Instance.currentCard = nextCard;
        Destroy(cardGameObject);
        CommonDisplay();
        CreateCard();

    }

    public void LoadScene()
    {
        GameManager.Instance.LoadGame();
        //SceneManager.LoadScene("CityBuilder");
    }

    public void ShowCharacterName(bool show)
    {
        characterNameText.gameObject.SetActive(show);
    }
}
