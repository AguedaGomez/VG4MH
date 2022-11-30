using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyQuestionsPanel_Decorator : MonoBehaviour
{
    public List<Questionnaire> dailyQuestionnaire;
    [SerializeField] GameObject questionPrefab;
    [SerializeField] GameObject questionnairePrefab;
    [SerializeField] Transform viewport;

    //List<QuestionDecorator> questionsInGame = new List<QuestionDecorator>();
    List<QuestionnaireDecorator> questionnaires = new List<QuestionnaireDecorator>();

    private void Start()
    {
        initializeQuestionsPanel();
    }

    void initializeQuestionsPanel()
    {
        foreach (Questionnaire questionnaire in dailyQuestionnaire)
        {
            GameObject questionnaireGO = Instantiate(questionnairePrefab, viewport);
            QuestionnaireDecorator questionnaireScript = questionnaireGO.GetComponent<QuestionnaireDecorator>();
            questionnaireScript.SetId(questionnaire.id);
            questionnaireScript.SetUpQuestionnaire(questionnaire.title, questionnaire.description, questionnaire.scaleExplanation);

            foreach (Question question in questionnaire.questions)
            {
                GameObject questionGO = Instantiate(questionPrefab, viewport);
                QuestionDecorator questionScript = questionGO.GetComponent<QuestionDecorator>();
                questionScript.setUpQuestion(question.description, question.id);

                Answer answer = questionScript.GetAnswer();
                questionnaireScript.AddAnswer(answer);
            }
            questionnaires.Add(questionnaireScript);

            
        }
    }

    public void confirmButton_Pressed()
    {
        SaveObject.Instance.dailyQuestions_Done = true;
        FindObjectOfType<City>().increaseActivationValue(20);

        CreateAnswerFullQuestionnaire();
        gameObject.SetActive(false);
    }

    private void CreateAnswerFullQuestionnaire()
    {
        string currentDate = DateTime.Now.ToString("d \nMMMM");
        AnswerFullQuestionnaire answerFullQuestionnaire = new AnswerFullQuestionnaire(currentDate, FindObjectOfType<City>().activationValue);

        foreach (QuestionnaireDecorator questionnaire in questionnaires)
        {
            answerFullQuestionnaire.score.Add(questionnaire.GetId(), questionnaire.GetTotalScore());
        }

        SaveObject.Instance.questionnairesDoneByUser.Add(answerFullQuestionnaire);
    }

    public void exitButton_Pressed()
    {
        Destroy(this.gameObject);
    }
}
