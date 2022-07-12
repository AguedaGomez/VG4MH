using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Cuestionario
{
    public string dateOfQuestionaire;
    //public Dictionary<string, Answer> answers = new Dictionary<string, Answer>();
    public float activationValue;
    public Answer PHQ_1_Answer;
    public Answer PHQ_2_Answer;
    public Answer GAD_1_Answer;
    public Answer GAD_2_Answer;

}
