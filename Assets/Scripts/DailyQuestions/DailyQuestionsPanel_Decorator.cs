using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyQuestionsPanel_Decorator : MonoBehaviour
{
    public List<DailyQuestion> dailyQuestions;
    [SerializeField] GameObject questionPrefab;
    [SerializeField] Transform viewport;

    List<QuestionDecorator> questionsInGame = new List<QuestionDecorator>();

    private void Start()
    {
        initializeQuestionsPanel();
    }

    void initializeQuestionsPanel()
    {
        for(int i = 0; i < dailyQuestions.Count; i++)
        {
            string newTitle;
            string newDescription;

            GameObject newQuestion = Instantiate(questionPrefab, viewport);

            newTitle = (i+1) + ". " + dailyQuestions[i].questionTitle;
            newDescription = dailyQuestions[i].questionDescription;

            QuestionDecorator newQuestionScript = newQuestion.GetComponent<QuestionDecorator>();
            newQuestionScript.setUpQuestion(newTitle, newDescription, dailyQuestions[i].questionId);
            questionsInGame.Add(newQuestionScript);
        }
    }

    public void confirmButton_Pressed()
    {
        SaveObject.Instance.dailyQuestions_Done = true;
        FindObjectOfType<City>().increaseActivationValue(20);

        CreateQuestionaire();
        Destroy(this.gameObject);
        //Se guardarán los datos en algún sitio
        //No sé aún cómo se traducirán los resultados
    }

    private void CreateQuestionaire()
    {
        Cuestionario newQuestionaire = new Cuestionario();
        //newQuestionaire.dateOfQuestionaire = DateTime.Now.ToString("d \nMMMM");
        newQuestionaire.dateOfQuestionaire = DateTime.Now.ToString("dd/MM");

        foreach (QuestionDecorator qDScript in questionsInGame )
        {
            //newQuestionaire.answers.Add(qDScript.GetAnswer().answerID, qDScript.GetAnswer());
            switch(qDScript.GetAnswer().answerID)
            {
                case "PHQ_1":
                    newQuestionaire.PHQ_1_Answer = qDScript.GetAnswer();
                    break;
                case "PHQ_2":
                    newQuestionaire.PHQ_2_Answer = qDScript.GetAnswer();
                    break;
                case "GAD_1":
                    newQuestionaire.GAD_1_Answer = qDScript.GetAnswer();

                    break;
                case "GAD_2":
                    newQuestionaire.GAD_2_Answer = qDScript.GetAnswer();

                    break;
            }
        }
        

        newQuestionaire.activationValue = FindObjectOfType<City>().activationValue;

        SaveObject.Instance.questionnairesDoneByUser.Add(newQuestionaire);
    }

    public void exitButton_Pressed()
    {
        Destroy(this.gameObject);
    }
}
