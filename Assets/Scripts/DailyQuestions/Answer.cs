using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Answer
{
    public string answerID;
    public int answerValue = 1;

    public Answer(string id)
    {
        answerID = id;
    }
}
