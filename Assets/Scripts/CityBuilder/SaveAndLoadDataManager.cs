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
        if (SaveAndLoadData.LoadFromFile("state-game.json", out var content))
        {
            //Comprobar si ha pasado un día para activar la posibilidad de volver 
            SaveDataToObject(JsonUtility.FromJson<SaveObject>(content));
            //Debug.Log("en Load game cuando se traduce el json: " + SaveObject.Instance.boardState.Count);
        }
            
        else
            SaveDataToObject(SaveObject.Instance);
        print("app abierta");

    }

    public void SaveGame()
    {
        ObjectToSaveData();
        SaveAndLoadData.SaveinFile("state-game.json", JsonUtility.ToJson(SaveObject.Instance));
        //Debug.Log("Pasos que se van a guardar: " + SaveObject.Instance.dailyCompletedSteps);

        //SaveObject.Instance.buildingsInBoard.Clear();
        print("app cerrada");
    }

    private void ObjectToSaveData()
    {

        //SaveObject.Instance.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        SaveObject.Instance.date = DateTime.Now.ToString();
        SaveObject.Instance.questionnairesDoneByUser_JSONReadable = SaveObject.Instance.questionnairesDoneByUser.ToArray();
        SaveObject.Instance.buildingsInBoard_JSONReadable = SaveObject.Instance.buildingsInBoard.ToArray();

        Debug.Log("Buildings , cuestionarios: " + SaveObject.Instance.buildingsInBoard_JSONReadable.Length + " , " + SaveObject.Instance.questionnairesDoneByUser_JSONReadable.Length);

        //Debug.Log("Activacion actual: " + SaveObject.Instance.activationValue);
        //Debug.Log("GUARDANDO Fecha guardada: " + SaveObject.Instance.date);
        //Debug.Log("Items que guarda el tablero: " + SaveObject.Instance.boardState.Count);
    }

    private void SaveDataToObject(SaveObject loadedSaveObject)
    {
        SceneManager.LoadScene("CityBuilder");
        SaveObject.Instance.materials = loadedSaveObject.materials;
        SaveObject.Instance.date = loadedSaveObject.date;
        Debug.Log("1. guardando fecha en objeto " + SaveObject.Instance.date);
        SaveObject.Instance.powerR = loadedSaveObject.powerR;
        SaveObject.Instance.activationValue = loadedSaveObject.activationValue;
        SaveObject.Instance.activityRunning = loadedSaveObject.activityRunning;
        SaveObject.Instance.dailyActivityCompleted = loadedSaveObject.dailyActivityCompleted;
        SaveObject.Instance.dailyCompletedSteps = loadedSaveObject.dailyCompletedSteps;
        SaveObject.Instance.actualSessionSteps = loadedSaveObject.actualSessionSteps;
        SaveObject.Instance.dailyQuestions_Done = loadedSaveObject.dailyQuestions_Done;
        SaveObject.Instance.buildingsInBoard = new List<SavedBuilding>(loadedSaveObject.buildingsInBoard_JSONReadable);
        SaveObject.Instance.questionnairesDoneByUser = new List<Cuestionario>(loadedSaveObject.questionnairesDoneByUser_JSONReadable);

        
        Debug.Log("Buildings , cuestionarios: " + SaveObject.Instance.buildingsInBoard.Count + " , " + SaveObject.Instance.questionnairesDoneByUser.Count);
    }
}
