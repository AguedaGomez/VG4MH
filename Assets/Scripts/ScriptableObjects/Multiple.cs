using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Multiple", menuName = "Card/Multiple", order = 1)]

public class Multiple : Card
{
    public List<Card> options;

    public Card nextCardIfRight;
    public Card nextCardIfLeft;
}
