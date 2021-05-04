using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveAndLoadDataManager : MonoBehaviour
{
    public Board board;
    public City city;

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) // Save
        {
            Debug.Log("TEST: en pausa, guardando el juego");
            ObjectToSaveData();
            SaveAndLoadData.SaveinFile("state-game.json", JsonUtility.ToJson(SaveObject.Instance));
        }
        else // Load
        {
            Debug.Log("TEST: load, cargando el juego");
            if (SaveAndLoadData.LoadFromFile("state-game.json", out var content))
                SaveDataToObject(JsonUtility.FromJson<SaveObject>(content));
            else
                SaveDataToObject(SaveObject.Instance);

        }
    }

    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        if (SaveAndLoadData.LoadFromFile("state-game.json", out var content))
    //            SaveDataToObject(JsonUtility.FromJson<SaveObject>(content));
    //        else
    //            SaveDataToObject(SaveObject.Instance);
    //        print("app abierta");
    //    }
    //    else
    //    {
    //        ObjectToSaveData();
    //        SaveAndLoadData.SaveinFile("state-game.json", JsonUtility.ToJson(SaveObject.Instance));
    //        print("app cerrada");
    //    }

    //}

    private void ObjectToSaveData()
    {
        SaveObject.Instance.materials = city.Materials;
        SaveObject.Instance.powerR = city.powerR;
        SaveObject.Instance.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        Debug.Log("Fecha guardada: " + SaveObject.Instance.date);
        board.SaveBoardStateInList(out SaveObject.Instance.boardState);
    }

    private void SaveDataToObject(SaveObject loadedSaveObject)
    {
        city.Materials = loadedSaveObject.materials;
        city.powerRLastCheckPoint = loadedSaveObject.powerR;
        city.InitializeCity(loadedSaveObject.date);
        board.InitializeBoard(loadedSaveObject.boardState);
    }


}
