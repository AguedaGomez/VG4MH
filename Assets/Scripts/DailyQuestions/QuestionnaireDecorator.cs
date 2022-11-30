using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionnaireDecorator : MonoBehaviour
{
    string id;

    [SerializeField] Text titleText;
    [SerializeField] Text descriptionText;
    [SerializeField] Text scaleText;

    List<Answer> answers= new List<Answer>();


    public void SetUpQuestionnaire(string title, string description, string scaleExplanation)
    {
        titleText.text = title;
        descriptionText.text = description;
        scaleText.text = scaleExplanation;
    }

    public void AddAnswer(Answer newAnswer)
    {
        answers.Add(newAnswer);
    }

    public int GetTotalScore()
    {
        int totalScore = 0;
        foreach (Answer a in answers)
        {
            totalScore += a.value;
        }
        return totalScore;
    }

    public string GetId() { return id; }

    public void SetId(string id) { this.id = id; }
}
