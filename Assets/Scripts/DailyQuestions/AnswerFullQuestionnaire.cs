using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AnswerFullQuestionnaire
{
    public string date;
    public Dictionary<string, int> score;
    public float activationValue;

    public AnswerFullQuestionnaire(string date, float activation)
    {
        score = new Dictionary<string, int>();
        this.date = date;
        activationValue = activation;
    }
}
