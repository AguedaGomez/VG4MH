using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Answer
{
    public string id;
    public int value;

    public Answer(string id)
    {
        this.id = id;
        value = 0; //No hay contestación
    }
}
