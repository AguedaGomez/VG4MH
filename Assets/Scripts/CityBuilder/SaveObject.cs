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

    private SaveObject()
    {

    }


    //public SaveObject(int materials, float powerR)
    //{
    //    this.materials = materials;
    //    this.powerR = powerR;
    //    date = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
    //    Debug.Log("Guardando fecha hoy: " + date);
    //}
  
}
