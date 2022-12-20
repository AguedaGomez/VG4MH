using System.Collections;
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
    public float energy;
    public string date;
    public List<SavedBuilding> buildingsInBoard; //id al construir e id de datos
    public List<AnswerFullQuestionnaire> questionnairesDoneByUser;
    public List<CharacterInfo> charactersInTheCity;
    public bool activityRunning;
    public bool dailyQuestions_Done;
    public bool enterInLibraryToday;
    public bool dailyActivityCompleted;
    public bool firstTimeInLibrary;
    public bool firstTimeInGameToday;
    public bool firstTimeInGame;
    public int dailyCompletedSteps;
    public int actualSessionSteps;
    

    private SaveObject()
    {
        materials = 0;
        powerR = 10;
        date = "";
        buildingsInBoard = new List<SavedBuilding>();
        questionnairesDoneByUser = new List<AnswerFullQuestionnaire>();
        charactersInTheCity = new List<CharacterInfo>();
        activityRunning = false;
        enterInLibraryToday = false;
        firstTimeInGameToday = false;
        firstTimeInLibrary = false;
        firstTimeInGame = true;
        energy = 0;
        dailyActivityCompleted = false;
        dailyQuestions_Done = false;
        dailyCompletedSteps = 0;
        actualSessionSteps = 0;
    }

    public List<Vector2> GetQuestionnaireValues(string idQuestionnaire)
    {
        List<Vector2> scoreListQuestionnaire = new List<Vector2>(); //lista de score de un cuestionario específico

        for (int i = 0; i < questionnairesDoneByUser.Count; i++)
        {
            Vector2 point = new Vector2();
            point.x = i;
            point.y = questionnairesDoneByUser[i].score[idQuestionnaire];
            scoreListQuestionnaire.Add(point);
        }
        return scoreListQuestionnaire;
        //int i = 0;
        //foreach(AnswerFullQuestionnaire newQuestionnaire in questionnairesDoneByUser)
        //{
        //    Answer currentAnsw = new Answer("Empty");

        //    switch (typeOfQuestion)
        //    {
        //        case "PHQ_1":
        //            currentAnsw = newQuestionnaire.PHQ_1_Answer;
        //            break;
        //        case "PHQ_2":
        //            currentAnsw = newQuestionnaire.PHQ_2_Answer;
        //            break;
        //        case "GAD_1":
        //            currentAnsw = newQuestionnaire.GAD_1_Answer;
        //            break;
        //        case "GAD_2":
        //            currentAnsw = newQuestionnaire.GAD_2_Answer;
        //            break;
        //    }

        //    Vector2 newValue = new Vector2();
        //    newValue.x = i;
        //    newValue.y = currentAnsw.answerValue;
        //    newListToReturn.Add(newValue);

        //    i++;
        //}


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

        //return newListToReturn;
    }
    public void ResetDay()
    {
        dailyActivityCompleted = false;
        dailyQuestions_Done = false;
        enterInLibraryToday = false;
        firstTimeInGameToday = false;
        firstTimeInLibrary = false;
        actualSessionSteps = 0;
        dailyCompletedSteps = 0;
    }

    public List<string> GetQuestionnaireDates()
    {
        List<string> dateList = new List<string>();

        foreach (AnswerFullQuestionnaire questionnaire in questionnairesDoneByUser)
        {
            dateList.Add(questionnaire.date);
        }

        return dateList;
    }

    public List<Vector2> GetQuestionnaireActivation()
    {
        List<Vector2> activationList = new List<Vector2>();

        int i = 0;
        foreach (AnswerFullQuestionnaire questionnaire in questionnairesDoneByUser)
        {
            Vector2 newValue = new Vector2(i, (questionnaire.activationValue * 4) / 100);
            activationList.Add(newValue);
        }

        return activationList;
    }

    //public void updateActivationValueOnLastQuestionnaire()
    //{
    //    if(dailyQuestions_Done)
    //    {
    //        questionnairesDoneByUser.Last().energy = energy;
    //    }
    //}
}
