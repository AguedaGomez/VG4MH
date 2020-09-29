using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Multiple", menuName = "Card/Multiple", order = 1)]

public class Multiple : Card
{
    public Choice choice1; //Top
    public Choice choice2;
    public Choice choice3;
    public Choice choice4; //Bottom
    public Card nextCardIfRight;
    public Card nextCardIfLeft;
}
