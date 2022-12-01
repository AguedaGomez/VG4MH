using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PageObject", menuName = "Library/Page", order = 1)]
public class PageObject : ScriptableObject
{
    public string pageTitle;
    [TextArea(10,14)]public string[] pageContent;

    public bool viewedByUser;
    public bool storyUnlocked;
}
