using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Single", menuName = "Card/Single", order = 1)]

public class Single : Card
{
    public Sprite image;
    public string rightText;
    public string leftText;

    public Direction correctDirection;

    public Card nextCardIfRight;
    public Card nextCardIfLeft;
}
