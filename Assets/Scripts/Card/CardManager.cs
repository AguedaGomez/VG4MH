using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{

    public GameObject singleCardPrefab;
    public GameObject multipleChoicePanel;

    //public GameObject multipleCardPrefab;

    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI characterNameText;
    public Transform parentOfCards;

    private GameObject cardGameObject;
    private CardDisplay cardDisplay;
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
    }
    public void CreateCard()
    {
        if (GameManager.Instance.currentCard.type == Card.CardType.SINGLE)
        {
            cardGameObject = Instantiate(singleCardPrefab, parentOfCards);
            multipleChoicePanel.SetActive(false);
        }

        else if (GameManager.Instance.currentCard.type == Card.CardType.MULTIPLE)
        {
            //cardGameObject = Instantiate(multipleCardPrefab, parentOfCards);
            multipleChoicePanel.SetActive(true);
            multipleChoiceDisplay.DisplayOptions();
        }
    }

    public void ShowCharacterName(bool show)
    {
        characterNameText.transform.parent.gameObject.SetActive(show);
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
}
