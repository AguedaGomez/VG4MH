using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject topHUD; //cuando haya que cambiar activacion, poder R y materiales.
    public GameObject downHUD;
    public GameObject confirmationMessage;
    public GameObject confirmationMssgQuestionnaire;
    [SerializeField] GameObject citizenTalkingPanel_Prefab;

    public InteractionController interactionController;
    public BuildingMenuController buildingMenuController;

    private DownHudController downHudController;
    private TOP_Hud_Controller topHudController;
    private Button actionButtons;
    public bool showingConversation = false;

    
    // Start is called before the first frame update
    void Start()
    {
        downHudController = downHUD.GetComponent<DownHudController>();
        buildingMenuController.CreateBuildingGrid();
        topHudController = topHUD.GetComponent<TOP_Hud_Controller>();
        actionButtons = downHUD.transform.Find("Actions Button").gameObject.GetComponent<Button>();
    }

    public void ShowQuestionnaireConfirmationMssg()
    {
        confirmationMssgQuestionnaire.SetActive(true);
    }

    public void ShowConfirmationMessage()
    {
        confirmationMessage.SetActive(true);
        //confirmationMessageController.ShowConfirmationMessage(true);
        downHudController.ShowAndHideActionsMenu(downHudController.activeSubmenu);
    }

    public void SaveBuildingToConstruct(Construction dataBuilding)
    {
        ShowConfirmationMessage();
        interactionController.SaveBuildingToConstruct(dataBuilding);
    }

    public void BuildConfirmation()
    {
        confirmationMessage.SetActive(false);
        interactionController.EnableBuilder(); //No es necesario buscar si están en la lista ni pasar el id porque está todo el SO accesible en GAmeManager
    }

    public void BuildNegation()
    {
        confirmationMessage.SetActive(false);
        GameManager.Instance.buildingInConstruction = null;
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

    public void createCitizenConversation(GameObject citizenToTalk)
    {
        if(!showingConversation)
        {
            showingConversation = true;
            GameObject newPanel = Instantiate(citizenTalkingPanel_Prefab, this.transform);
            newPanel.GetComponent<characterTalkPanel_Manager>().setUpPanel(citizenToTalk);
        }
    }

    void Update()
    {
        if (GameManager.Instance.buildingInConstruction != null) actionButtons.interactable = false;
        else actionButtons.interactable=true;
    }
        
}


