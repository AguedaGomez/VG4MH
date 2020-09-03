using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Single", menuName = "Card/Single", order = 1)]

public class Single : Card
{
    public Choice choice;
    public Card nextCardIfAnswerCorrect;
    public Card nextCardIfAnswerIncorrect;
}
