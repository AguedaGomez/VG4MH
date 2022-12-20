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
        PHYSICALACT,
        POSITIVE,
        NONE
    }

    public Resource reward;

    public enum Direction
    {
        RIGHT,
        LEFT,
        BOTH,
        NONE
    }

    public enum CardType
    {
        SINGLE,
        MULTIPLE,
        MULTIPLE_ONE
    }

    public CardType type;
}
