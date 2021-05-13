using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject
{
    public string characterName;
    [TextArea(10, 14)] public string dialog;

    public enum Resource
    {
        MOTIVATION,
        FLEXIBILITY,
        ACTIVATION,
        POSITIVE,
        NONE
    }

    public Resource reward;

    public enum CardType
    {
        SINGLE,
        MULTIPLE
    }

    public CardType type;
}
