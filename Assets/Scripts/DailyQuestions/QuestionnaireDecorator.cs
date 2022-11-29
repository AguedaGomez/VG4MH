using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionnaireDecorator : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Text descriptionText;
    // Start is called before the first frame update

    public void SetUpQuestionnaire(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;
    }
}
