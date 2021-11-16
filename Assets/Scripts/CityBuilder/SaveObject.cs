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
    public string date;
    public List<SavedBuilding> buildingsInBoard;
    

    private SaveObject()
    {
        materials = 0;
        powerR = 0;
        date = "";
        buildingsInBoard = new List<SavedBuilding>();
    }
  
}
