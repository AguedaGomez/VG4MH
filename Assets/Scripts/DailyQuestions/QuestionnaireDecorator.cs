using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionnaireDecorator : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Text descriptionText;
    [SerializeField] Text scaleText;
    // Start is called before the first frame update

    public void SetUpQuestionnaire(string title, string description, string scaleExplanation)
    {
        titleText.text = title;
        descriptionText.text = description;
        scaleText.text = scaleExplanation;
    }
}
