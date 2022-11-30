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

    List<QuestionDecorator> questionsInGame = new List<QuestionDecorator>();

    private void Start()
    {
        initializeQuestionsPanel();
    }

    void initializeQuestionsPanel()
    {
        foreach (Questionnaire questionnaire in dailyQuestionnaire)
        {
            GameObject questionnaireGO = Instantiate(questionnairePrefab, viewport);
            questionnaireGO.GetComponent<QuestionnaireDecorator>().SetUpQuestionnaire(questionnaire.title, questionnaire.description, questionnaire.scaleExplanation);

            foreach (Question question in questionnaire.questions)
            {
                GameObject questionGO = Instantiate(questionPrefab, viewport);
                QuestionDecorator questionScript = questionGO.GetComponent<QuestionDecorator>();
                questionScript.setUpQuestion(question.description, question.id);
                //questionsInGame.Add(newQuestionScript);
            }

            
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
