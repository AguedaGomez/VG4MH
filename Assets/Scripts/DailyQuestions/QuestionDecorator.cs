using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionDecorator : MonoBehaviour
{
    [SerializeField] Text questionDescription_Text;
    [SerializeField] Slider questionSlider;

    Answer answer;

    public void setUpQuestion(string description,string answerId)
    {
        questionDescription_Text.text = description;

        answer = new Answer(answerId);
    }

    public Answer GetAnswer()
    {
        return answer;
    }

    public void SetAnswerValue()
    {
        answer.value = (int)questionSlider.value;
        //Debug.Log("Nuevo valor ID: " + questionAnswer.answerID + " , " + questionAnswer.answerValue);
    }
}
