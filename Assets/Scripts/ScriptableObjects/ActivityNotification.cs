using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopUp_Notification", menuName = "Activity Notification Data", order = 1)]

public class ActivityNotification : Notification
{
    public int stepsToShow;
    public bool hasBeenShown;
}
