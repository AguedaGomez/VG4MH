using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SaveAndLoadDataManager : MonoBehaviour
{
    public Board board;
    public City city;

#if !UNITY_EDITOR

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) // Save
        {
            if (SaveObject.Instance.activityRunning)
            {
                ActivityService activityService = FindObjectOfType<ActivityService>();
                activityService.SyncData();
            }
            SaveGame();
        }
        else // Load
        {
            LoadGame();
        }
    }
#elif UNITY_EDITOR && UNITY_ANDROID

    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        LoadGame();
    //    }
    //    else
    //    {
    //        SaveGame();
    //    }

    //}
#endif

    public void LoadGame()
    {


        //Debug.Log("Existe city" + city.gameObject + " , Existe Board: " + board.gameObject);
        if (SaveAndLoadData.LoadFromFile("state-game.json", out var content))
        {
            //Comprobar si ha pasado un día para activar la posibilidad de volver 
            SaveDataToObject(JsonUtility.FromJson<SaveObject>(content));
            city = FindObjectOfType<City>();
            board = FindObjectOfType<Board>();
            //Debug.Log("en Load game cuando se traduce el json: " + SaveObject.Instance.boardState.Count);
        }
            
        else
            SaveDataToObject(SaveObject.Instance);
        print("app abierta");

    }

    public void SaveGame()
    {
        //ObjectToSaveData();
        SaveAndLoadData.SaveinFile("state-game.json", JsonUtility.ToJson(SaveObject.Instance));
        //SaveObject.Instance.buildingsInBoard.Clear();
        print("app cerrada");
    }

    /*void showQuestionnairesDEBUG()
    {
        if(SaveObject.Instance.questionnairesDoneByUser != null || SaveObject.Instance.questionnairesDoneByUser.Count != 0)
        {
            foreach (Cuestionario quest in SaveObject.Instance.questionnairesDoneByUser)
            {
                Debug.Log("Questionario : " + quest.dateOfQuestionaire + " , " + quest.energy);
            }
        }else
        {
            Debug.Log("No existe lista de cuestionarios");
        }
        
    }

    void showBuildings_Debug()
    {
        if(SaveObject.Instance.buildingsInBoard != null || SaveObject.Instance.buildingsInBoard.Count != 0)
        {
            foreach (SavedBuilding build in SaveObject.Instance.buildingsInBoard)
            {
                Debug.Log("Building info: " + build.buildingName + " , " + build.id + " , " + build.currentMaterials);
            }
        }else
        {
            Debug.Log("No existe lista de edificios");
        }
      *  
    }*/

    private void ObjectToSaveData()
    {
        city = FindObjectOfType<City>();
        board = FindObjectOfType<Board>();
        //SaveObject.Instance.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        SaveObject.Instance.date = DateTime.Now.ToString();
        SaveObject.Instance.energy = city.Energy;
        
       // Debug.Log("Buildings , cuestionarios: " + SaveObject.Instance.buildingsInBoard.Count + " , " + SaveObject.Instance.questionnairesDoneByUser.Count);

        //Debug.Log("Activacion actual: " + SaveObject.Instance.energy);
        //Debug.Log("GUARDANDO Fecha guardada: " + SaveObject.Instance.date);
        //Debug.Log("Items que guarda el tablero: " + SaveObject.Instance.boardState.Count);
    }

    private void SaveDataToObject(SaveObject loadedSaveObject)
    {
        SceneManager.LoadScene(SaveObject.Instance.currentScene);
        SaveObject.Instance.materials = loadedSaveObject.materials;
        SaveObject.Instance.date = loadedSaveObject.date;
        Debug.Log("1. guardando fecha en objeto " + SaveObject.Instance.date);
        SaveObject.Instance.powerR = loadedSaveObject.powerR;
        SaveObject.Instance.energy = loadedSaveObject.energy;
        SaveObject.Instance.activityRunning = loadedSaveObject.activityRunning;
        SaveObject.Instance.dailyActivityCompleted = loadedSaveObject.dailyActivityCompleted;
        SaveObject.Instance.dailyCompletedSteps = loadedSaveObject.dailyCompletedSteps;
        SaveObject.Instance.actualSessionSteps = loadedSaveObject.actualSessionSteps;
        SaveObject.Instance.dailyQuestions_Done = loadedSaveObject.dailyQuestions_Done;
        SaveObject.Instance.enterInLibraryToday = loadedSaveObject.enterInLibraryToday;
        SaveObject.Instance.buildingsInBoard = loadedSaveObject.buildingsInBoard;
        SaveObject.Instance.currentScene = loadedSaveObject.currentScene;
        SaveObject.Instance.firstTimeInGame = loadedSaveObject.firstTimeInGame;
        SaveObject.Instance.firstTimeInGameToday = loadedSaveObject.firstTimeInGameToday;
        SaveObject.Instance.firstTimeInLibrary = loadedSaveObject.firstTimeInLibrary;
 //       SaveObject.Instance.questionnairesDoneByUser = loadedSaveObject.questionnairesDoneByUser;
        SaveObject.Instance.charactersInTheCity = loadedSaveObject.charactersInTheCity;

        //Debug.Log("Buildings , cuestionarios: " + SaveObject.Instance.buildingsInBoard.Count + " , " + SaveObject.Instance.questionnairesDoneByUser.Count);
    }

    
}
