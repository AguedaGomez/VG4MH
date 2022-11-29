using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationMessageController : MonoBehaviour
{
    public Text textMessage;
    public Button NoButton;
    public Button YesButton;

    private const string TEXT_MESSAGE = "¿Quieres realizar esta construcción?"; 
    void Start()
    {
        SetUpMessage();
    }

    public void SetUpMessage()
    {
        textMessage.text = TEXT_MESSAGE;
        //NoButton.onClick.AddListener()
    }
    
}
