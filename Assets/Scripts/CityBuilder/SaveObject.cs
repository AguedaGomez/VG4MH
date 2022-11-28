﻿using System.Collections;
using System;
using System.Linq;
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
    public List<SavedBuilding> buildingsInBoard; //id al construir e id de datos
    public List<Cuestionario> questionnairesDoneByUser;
    public List<CharacterInfo> charactersInTheCity;
    public bool activityRunning;
    public bool dailyQuestions_Done;
    public bool enterInLibraryToday;
    public bool dailyActivityCompleted;
    public int dailyCompletedSteps;
    public int actualSessionSteps;
    

    private SaveObject()
    {
        materials = 0;
        powerR = 0;
        date = "";
        buildingsInBoard = new List<SavedBuilding>();
        questionnairesDoneByUser = new List<Cuestionario>();
        charactersInTheCity = new List<CharacterInfo>();
        activityRunning = false;
        enterInLibraryToday = false;
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

    public List<string> getSpecifiedQuestionnaire_Date()
    {
        List<string> newListToReturn = new List<string>();

        foreach (Cuestionario newQuestionnaire in questionnairesDoneByUser)
        {
            string currentAnsw = newQuestionnaire.dateOfQuestionaire.ToString();
            newListToReturn.Add(currentAnsw);
        }

        return newListToReturn;
    }

    public List<Vector2> getActivationValues()
    {
        List<Vector2> listToReturn = new List<Vector2>();

        int i = 0;
        foreach(Cuestionario quest in questionnairesDoneByUser)
        {
            Vector2 newValue = new Vector2(i, (quest.activationValue * 4) / 100);
            listToReturn.Add(newValue);
        }

        return listToReturn;
    }

    public void updateActivationValueOnLastQuestionnaire()
    {
        if(dailyQuestions_Done)
        {
            questionnairesDoneByUser.Last().activationValue = activationValue;
        }
    }
}
