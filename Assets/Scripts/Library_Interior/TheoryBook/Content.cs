using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Content", menuName = "Library/Content", order = 1)]
public class Content : ScriptableObject
{
    public string title;
    public List<PageObject> pages;
}
