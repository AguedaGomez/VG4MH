using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int materials;
    public float powerR;
    public DateTime date; // para guardar, pasar a string?
    // save fecha y hora

    public SaveData(int materials, float powerR)
    {
        this.materials = materials;
        this.powerR = powerR;
        //date = DateTime.Today;
    }
  
}
