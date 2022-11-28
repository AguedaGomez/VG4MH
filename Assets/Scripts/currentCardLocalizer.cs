using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentCardLocalizer : MonoBehaviour
{
    [SerializeField] Button canvasButton;
    [SerializeField] Image exclamationImage;
    [SerializeField] GameObject cameraToMove;
    private float xOffset = 20;
    private float zOffset = 20;
    public GameObject citizenToLocalize;

    public void changeButtonState(bool newState)
    {
        canvasButton.interactable = newState;
        if(newState)
        {
            LeanTween.scale(canvasButton.gameObject, new Vector3(1, 1, 1), 1);
            LeanTween.scale(exclamationImage.gameObject, new Vector3(1, 1, 1), 1);
        }else
        {
            LeanTween.scale(canvasButton.gameObject, new Vector3(0.5f, 0.5f, 0.5f), 1);
            LeanTween.scale(exclamationImage.gameObject, new Vector3(0, 0, 0), 0.5f);
        }
    }

    public void setNewCitizenToLocate(GameObject newCitizen)
    {
        citizenToLocalize = newCitizen;
    }

    public void localizeCurrentCardCharacter()
    {
        if(citizenToLocalize != null)
        {
            Vector3 vectorToMove = new Vector3(citizenToLocalize.transform.localPosition.x - xOffset, cameraToMove.transform.localPosition.y, citizenToLocalize.transform.localPosition.z - zOffset);

            LeanTween.move(cameraToMove, vectorToMove, 1f);

            LeanTween.scale(canvasButton.gameObject, new Vector3(0.75f, 0.75f, 0.75f), 0.5f).setOnComplete(() => LeanTween.scale(canvasButton.gameObject, new Vector3(1, 1, 1), 0.5f));

        }
    }
}
