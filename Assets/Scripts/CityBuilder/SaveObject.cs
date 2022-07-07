using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class SaveObject
{
    public static SaveObject Instance { get; private set; } = new SaveObject();

    public int materials;
    public float powerR;
    public float activationValue;
    public string date;
    public List<SavedBuilding> buildingsInBoard;
    public List<Cuestionario> questionnairesDoneByUser;
    public bool activityRunning;
    public bool dailyQuestions_Done;
    public bool dailyActivityCompleted;
    public int dailyCompletedSteps;
    public int actualSessionSteps;
    public SavedBuilding[] buildingsInBoard_JSONReadable;
    public Cuestionario[] questionnairesDoneByUser_JSONReadable;

    private SaveObject()
    {
        materials = 0;
        powerR = 0;
        date = "";
        buildingsInBoard = new List<SavedBuilding>();
        questionnairesDoneByUser = new List<Cuestionario>();
        activityRunning = false;
        activationValue = 0;
        dailyActivityCompleted = false;
        dailyQuestions_Done = false;
        dailyCompletedSteps = 0;
        actualSessionSteps = 0;
    }

    public List<Vector2> getSpecifiedQuestionnaires(string typeOfQuestion)
    { 
        List<Vector2> newListToReturn = new List<Vector2>();

        int i = 0;
        foreach(Cuestionario newQuestionnaire in questionnairesDoneByUser)
        {
            Answer currentAnsw = new Answer("Empty");

            switch (typeOfQuestion)
            {
                case "PHQ_1":
                    currentAnsw = newQuestionnaire.PHQ_1_Answer;
                    break;
                case "PHQ_2":
                    currentAnsw = newQuestionnaire.PHQ_2_Answer;
                    break;
                case "GAD_1":
                    currentAnsw = newQuestionnaire.GAD_1_Answer;
                    break;
                case "GAD_2":
                    currentAnsw = newQuestionnaire.GAD_2_Answer;
                    break;
            }

            Vector2 newValue = new Vector2();
            newValue.x = i;
            newValue.y = currentAnsw.answerValue;
            newListToReturn.Add(newValue);

            i++;
        }


        /*for (int i = 0; i < savedataLists.questionnairesDoneByUser.Count; i++)
        {
            Debug.Log("Procesando dato número: " + i);
            Answer currentAnsw = new Answer("Empty");

            switch (typeOfQuestion)
            {
                case "PHQ_1":
                    currentAnsw = savedataLists.questionnairesDoneByUser[i].PHQ_1_Answer;
                    break;
                case "PHQ_2":
                    currentAnsw = savedataLists.questionnairesDoneByUser[i].PHQ_2_Answer;
                    break;
                case "GAD_1":
                    currentAnsw = savedataLists.questionnairesDoneByUser[i].GAD_1_Answer;

                    break;
                case "GAD_2":
                    currentAnsw = savedataLists.questionnairesDoneByUser[i].GAD_2_Answer;

                    break;

            }


            Vector2 newValue = new Vector2();
            newValue.x = i;
            newValue.y = currentAnsw.answerValue;

            newListToReturn.Add(newValue);
        }*/

        return newListToReturn;
    }
}
