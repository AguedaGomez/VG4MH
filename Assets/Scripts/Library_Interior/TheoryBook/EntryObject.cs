using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntryObject", menuName = "Library/Entry", order = 1)]
public class EntryObject : ScriptableObject
{
    public string title;
    public List<Content> content;
    public bool unlocked;
    public bool read;
    public Card.Resource typeContent;
}
