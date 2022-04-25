using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject topHUD; //cuando haya que cambiar activacion, poder R y materiales.
    public GameObject downHUD;
    public GameObject confirmationMessage;

    public InteractionController interactionController;

    private DownHudController downHudController;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start canvascontroller");
        downHudController = downHUD.GetComponent<DownHudController>();
    }

    public void ShowConfirmationMessage()
    {
        confirmationMessage.SetActive(true);
        //confirmationMessageController.ShowConfirmationMessage(true);
        downHudController.ShowAndHideActionsMenu(downHudController.activeSubmenu);
    }

    public void SaveBuildingToConstruct(Construction construct)
    {
        ShowConfirmationMessage();
        interactionController.SaveBuildingToConstruct(construct);
    }

    public void BuildConfirmation()
    {
        confirmationMessage.SetActive(false);
        interactionController.EnableBuilder(GameManager.Instance.buildingInConstruction.id); //No es necesario buscar si están en la lista ni pasar el id porque está todo el SO accesible en GAmeManager
    }


}


