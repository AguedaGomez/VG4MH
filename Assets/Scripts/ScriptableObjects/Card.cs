using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject
{
    public string characterName;
    public string dialog;

    public enum Resource
    {
        MOTIVATION,
        FLEXIBILITY,
        ACTIVATION,
        POSITIVITY,
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
