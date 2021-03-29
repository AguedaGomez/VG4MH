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
            print("save");
            //SaveAndLoadData.SaveinFile("state-game", );
        }
        else // Load
        {
            print("load");
            
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            SaveAndLoadData.LoadFromFile("state-game.json", out var content);
            SaveDataToObject(JsonUtility.FromJson<SaveObject>(content));
            print("app abierta");
        }   
        else
        {
            ObjectToSaveData();
            SaveAndLoadData.SaveinFile("state-game.json", JsonUtility.ToJson(SaveObject.Instance));
            print("app cerrada");
        }
    
    }

    private void ObjectToSaveData()
    {
        SaveObject.Instance.materials = city.Materials;
        SaveObject.Instance.powerR = city.powerR;
        SaveObject.Instance.date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }

    private void SaveDataToObject(SaveObject loadedSaveObject)
    {
        city.Materials = loadedSaveObject.materials;
        city.powerR = loadedSaveObject.powerR;
        city.InitializeCity(loadedSaveObject.date);

    }
}
