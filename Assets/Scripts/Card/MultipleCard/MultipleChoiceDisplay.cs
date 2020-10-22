using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class MultipleChoiceDisplay : CardDisplay, IDisplay
{
    public TextMeshPro generalText;
    public enum ChoicePosition
    {
        TOP,
        MIDDLE_TOP,
        MIDDLE_BOTTOM,
        BOTTOM
    }
    public ChoicePosition currentChoice; // cambiar esto

    private Choice myChoice;
    private SortingGroup sortingGroup;

    protected override void Start()
    {
        base.Start();
        sortingGroup = GetComponent<SortingGroup>();
    }
    public override void ShowOption(Choice.Direction direction)
    {
        sortingGroup.sortingOrder = 1;
        base.ShowOption(direction);
        sortingGroup.sortingOrder = 0;

    }
    public override void DisplayCard()
    {
        // preparing camera for initial movement of card
        Camera.main.orthographic = false;
        startedInitialMovement = true;

        GetChoice(currentChoice);

        // preparing card data
        generalText.text = myChoice.text;
        rightOptionText.text = myChoice.rightText;
        leftOptionText.text = myChoice.leftText;
        image.sprite = myChoice.image;
    }

    private void GetChoice(ChoicePosition currentChoice)
    {
        switch (currentChoice)
        {
            case ChoicePosition.TOP:
                myChoice = ((Multiple)currentCard).choice1;
                break;
            case ChoicePosition.MIDDLE_TOP:
                myChoice = ((Multiple)currentCard).choice2;
                break;
            case ChoicePosition.MIDDLE_BOTTOM:
                myChoice = ((Multiple)currentCard).choice3;
                break;
            case ChoicePosition.BOTTOM:
                myChoice = ((Multiple)currentCard).choice4;
                break;
            default:
                myChoice = null;
                break;
        }
        return;
    }
}
