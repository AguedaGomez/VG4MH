using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalsMessages : MonoBehaviour
{
    public delegate void GeneralMessageEventHandler(string message);
    public event GeneralMessageEventHandler ShowGeneralMessage;
    public Button conflictExclamation;
    public bool specialCitizenWithConflict;
    public bool isSpecialCitizen = false;

    private RectTransform conflictExclamationRect;

    private const int minConflictSec = 15;
    private const int maxConflictSec = 60;
    private string[] generalMessages = { "Hola, ¿qué tal?", "¡Qué buen día hace hoy!", "¡Qué bien!, tú por aquí",
    "¿Has visitado ya la biblioteca?", "¿Cómo te va?"};
    private string[] generalMissionMessages = { "¿Puedes ayudarme?", "Tengo algo que consultarte, ¿Tienes un momento?", 
    "¿Puedes echarme una mano?", "¿Me ayudas?"};


    // Start is called before the first frame update
    void Start()
    {
        conflictExclamationRect = conflictExclamation.transform.GetComponentInParent<RectTransform>();
    }
    private void Update()
    {
        if (conflictExclamation == null) 
        {
            Debug.LogError("Nullreference", conflictExclamation);
        }

        var positionToLook = Camera.main.transform.position;

        if(conflictExclamation.gameObject.active)
        {
            var previousRotation = conflictExclamationRect.eulerAngles;

            conflictExclamationRect.LookAt(positionToLook);
            conflictExclamationRect.eulerAngles = new Vector3(previousRotation.x, conflictExclamationRect.eulerAngles.y, previousRotation.z);
        }
    }
    public void GenerateGeneralMessage()
    {
        int randIndex = Random.Range(0, generalMessages.Length);
        ShowGeneralMessage(generalMessages[randIndex]);
    }

    public string getRandomMessage()
    {
        int randIndex = Random.Range(0, generalMessages.Length);
        return generalMessages[randIndex];
    }

    public string getMissionRandomMessage()
    {
        int randIndex = Random.Range(0, generalMissionMessages.Length);
        return generalMissionMessages[randIndex];
    }

    public void ShowConflictExclamation(bool newState)
    {
        conflictExclamation.gameObject.SetActive(newState);
        specialCitizenWithConflict = newState;
    }

    public void citizen_OnClick(GameObject citizenToTalk)
    {
        //Crear la notificación de abajo y demás
        CanvasController mainCanvas = (CanvasController)FindObjectOfType(typeof(CanvasController));

        mainCanvas.createCitizenConversation(citizenToTalk);
    }
}
