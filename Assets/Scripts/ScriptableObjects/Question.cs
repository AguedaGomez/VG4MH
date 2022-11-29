using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question", order = 1)]
public class Question : ScriptableObject
{

    public string description;
    public string id;
}
