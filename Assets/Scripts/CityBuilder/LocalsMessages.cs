using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalsMessages : MonoBehaviour
{
    public delegate void GeneralMessageEventHandler(string message);
    public event GeneralMessageEventHandler ShowGeneralMessage;
    public Button conflictExclamation;

    private const int minConflictSec = 15;
    private const int maxConflictSec = 60;
    private string[] generalMessages = { "Hola, ¿qué tal?", "¡Qué buen día hace hoy!", "¡Qué bien!, tú por aquí",
    "¿Has visitado ya la biblioteca?", "¿Cómo te va?"};
    // Start is called before the first frame update
    void Start()
    {
    }

    public void GenerateGeneralMessage()
    {
        int randIndex = Random.Range(0, generalMessages.Length);
        ShowGeneralMessage(generalMessages[randIndex]);
    }

    public void ShowConflictExclamation()
    {
        conflictExclamation.gameObject.SetActive(true);
    }
}
