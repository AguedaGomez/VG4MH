using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "History Page", menuName = "Library Objects", order = 1)]
public class PageObject : ScriptableObject
{
    public string pageTitle;
    public string pageContent;

    public bool viewedByUser;
    public bool storyUnlocked;
}
