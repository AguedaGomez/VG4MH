using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadDataManager : MonoBehaviour
{
    public Board board;
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
            SaveAndLoadData.LoadFromFile("state-game.dat", out var content);
            SaveData loadedSaveObject = JsonUtility.FromJson<SaveData>(content);
            print("app abierta");
        }   
        else
        {
            SaveAndLoadData.SaveinFile("state-game.dat", board.city.DataToJson());
            print("app cerrada");
        }
    
    }
}
