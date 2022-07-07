using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyQuestion", menuName = "Daily Questions", order = 1)]
public class DailyQuestion : ScriptableObject
{
    public string questionTitle;
    public string questionDescription;
    public string questionId;
}
