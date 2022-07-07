using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PageController : MonoBehaviour
{
    [SerializeField] private Animation turnPageAnimation;
    [SerializeField] private Book book;
    [SerializeField] List<GameObject> canvasButtons;

    List<bool> previousButtonStates;

    
    public void TurnPHQ_Page()
    {
        book.Hide_UI(false);
        turnPageAnimation.Play();
    }
    public void HideUI()
    {
        book.Hide_UI(false);
    }
    public void UnhideUI()
    {
        book.Hide_UI(true);
    }

    public void TurnGAD_Page()
    {
        book.Hide_UI(false);
        turnPageAnimation.Play();
    }

    public void turnPageAnimationStart() { turnPageAnimation.Play(); }

    public void activateButtonsInteractivity()
    {
        setCanvasButtonsInteractivity(true);
        for(int i = 0; i < previousButtonStates.Count; i++)
        {
            canvasButtons[i].GetComponent<Button>().interactable = previousButtonStates[i];
        }
        previousButtonStates.Clear();
    }

    public void desactivateButtonsInteractivity()
    {
        previousButtonStates = new List<bool>();
        foreach (GameObject button in canvasButtons)
        {
            previousButtonStates.Add(button.GetComponent<Button>().interactable);
        }
        setCanvasButtonsInteractivity(false);
    }

    public void setCanvasButtonsInteractivity(bool newState)
    {
        foreach (GameObject button in canvasButtons)
        {
            button.GetComponent<Button>().interactable = newState;
        }
    }
}
