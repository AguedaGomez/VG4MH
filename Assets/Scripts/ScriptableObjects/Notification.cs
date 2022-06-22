using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopUp_Notification", menuName = "Notification Data", order = 1)]
public class Notification : ScriptableObject
{
    public Sprite imageNotification;
    public string textNotification;
}
