using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
#elif UNITY_EDITOR && UNITY_ANDROID

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            LoadGame();
        }
        else
        {
            SaveGame();
        }

    }
#endif

    public void LoadGame()
    {
        if (SaveAndLoadData.LoadFromFile("state-game.json", out var content))
        {
            
            SaveDataToObject(JsonUtility.FromJson<SaveObject>(content));
            Debug.Log("en Load game cuando se traduce el json: " + SaveObject.Instance.boardState.Count);
        }
            
        else
            SaveDataToObject(SaveObject.Instance);
        print("app abierta");
    }

    public void SaveGame()
    {
        ObjectToSaveData();
        SaveAndLoadData.SaveinFile("state-game.json", JsonUtility.ToJson(SaveObject.Instance));
        SaveObject.Instance.boardState.Clear();
        print("app cerrada");
    }

    private void ObjectToSaveData()
    {
        
        SaveObject.Instance.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //Debug.Log("GUARDANDO Fecha guardada: " + SaveObject.Instance.date);
        Debug.Log("Items que guarda el tablero: " + SaveObject.Instance.boardState.Count);
    }

    private void SaveDataToObject(SaveObject loadedSaveObject)
    {
        SaveObject.Instance.materials = loadedSaveObject.materials;
        SaveObject.Instance.date = loadedSaveObject.date;
        SaveObject.Instance.boardState = loadedSaveObject.boardState;
        Debug.Log("1. Cargando datos, boardState.Count " + SaveObject.Instance.boardState.Count);
    }


}
