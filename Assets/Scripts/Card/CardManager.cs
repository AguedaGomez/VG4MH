using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    public GameObject singleCardPrefab;
    public GameObject multipleCardPrefab;

    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI characterNameText;

    private GameObject cardGameObject;
    private SingleCardDisplay singleCardDisplay;

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
            singleCardDisplay = cardGameObject.GetComponent<SingleCardDisplay>();
            singleCardDisplay.DestroyCardEvent += CardManager_DestroyCardEvent;
        }
           
        else if (GameManager.Instance.currentCard.type == Card.CardType.MULTIPLE)
            cardGameObject = Instantiate(multipleCardPrefab, transform);
        
    }

    private void CardManager_DestroyCardEvent()
    {
        singleCardDisplay.DestroyCardEvent -= CardManager_DestroyCardEvent;
        Destroy(cardGameObject);
        CommonDisplay();
        CreateCard();
    }
}
