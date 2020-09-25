﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Choice", menuName ="Choice", order =2)]
public class Choice : ScriptableObject
{
    public enum Direction
    {
        RIGHT,
        LEFT,
        BOTH,
        NONE
    }

    public Sprite image;
    public string text; // for multiple card only
    public string rightText;
    public string leftText;
    public Direction correctDirection;
}
