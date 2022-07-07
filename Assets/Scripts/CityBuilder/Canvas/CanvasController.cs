﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject topHUD; //cuando haya que cambiar activacion, poder R y materiales.
    public GameObject downHUD;
    public GameObject confirmationMessage;

    public InteractionController interactionController;
    public BuildingMenuController buildingMenuController;

    private DownHudController downHudController;
    private TOP_Hud_Controller topHudController;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start canvascontroller");
        downHudController = downHUD.GetComponent<DownHudController>();
        buildingMenuController.CreateBuildingGrid();
        topHudController = topHUD.GetComponent<TOP_Hud_Controller>();
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
        interactionController.EnableBuilder(); //No es necesario buscar si están en la lista ni pasar el id porque está todo el SO accesible en GAmeManager
    }

    public void UnlockBuilding(string id)
    {
        buildingMenuController.UnlockGridElement(id);
    }

    public void updateSlidersValue(float newActivationValue = -1, float newPowerR_Value = -1)
    {
        if (newActivationValue > 0)
        {
            topHudController.updateActivationSliderValue(newActivationValue);
        }
        if (newPowerR_Value > 0)
        {
            topHudController.updatePowerR_SliderValue(newPowerR_Value);
        }
    }
}


