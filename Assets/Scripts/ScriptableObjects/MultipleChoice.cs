using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MultipleChoice", menuName = "MultipleChoice", order = 2)]

public class MultipleChoice : ScriptableObject
{
    [TextArea(10, 14)] public string dialog;

    public Card option1;
    public Card option2;
    public Card option3;
    public Card option4;

}
