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
    public GameObject multipleCardPrefab;

    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI characterNameText;

    private GameObject cardGameObject;
    private CardDisplay cardDisplay;

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
            cardGameObject = Instantiate(singleCardPrefab, transform);
        }

        else if (GameManager.Instance.currentCard.type == Card.CardType.MULTIPLE)
            cardGameObject = Instantiate(multipleCardPrefab, transform);

    }

    public void UpdateCurrentCard(Card nextCard)
    {
        if (GameManager.Instance.currentCard.type == Card.CardType.SINGLE)
        {
            GameManager.Instance.currentCard = nextCard;
            Destroy(cardGameObject);
            CommonDisplay();
            CreateCard();
        }
        // Qué pasa en el caso múltiple
    }

    public void LoadScene()
    {
        GameManager.Instance.LoadGame();
        //SceneManager.LoadScene("CityBuilder");
    }
}
