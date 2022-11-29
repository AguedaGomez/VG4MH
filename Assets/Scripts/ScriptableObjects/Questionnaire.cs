using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questionnaire", menuName = "Questionnaire", order = 1)]
public class Questionnaire : ScriptableObject
{
    public string title;
    public string description;
    public List<Question> questions;
}
