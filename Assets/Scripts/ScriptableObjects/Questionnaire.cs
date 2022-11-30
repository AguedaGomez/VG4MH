using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questionnaire", menuName = "Questionnaire", order = 1)]
public class Questionnaire : ScriptableObject
{
    public string title;
    [TextArea(10, 14)] public string description;
    [TextArea(10, 14)] public string scaleExplanation;
    public List<Question> questions;
}
