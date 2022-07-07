using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionDecorator : MonoBehaviour
{
    [SerializeField] Text titleQuestion_Text;
    [SerializeField] Text questionDescription_Text;
    [SerializeField] Slider questionSlider;

    Answer questionAnswer;

    public void setUpQuestion(string title, string description,string answerId)
    {
        titleQuestion_Text.text = title;
        questionDescription_Text.text = description;

        questionAnswer = new Answer(answerId);
    }

    public Answer GetAnswer()
    {
        return questionAnswer;
    }

    public void SetAnswerValue()
    {
        questionAnswer.answerValue = (int)questionSlider.value;
        //Debug.Log("Nuevo valor ID: " + questionAnswer.answerID + " , " + questionAnswer.answerValue);
    }
}
