using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityPanel_Decorator : MonoBehaviour
{
    [SerializeField] CanvasController canvasManager;
    [SerializeField] TOP_Hud_Controller topHud_Controller;
    [SerializeField] DownHudController downHud_Controller;
    [SerializeField] ActivityService podometerManager;

    [SerializeField] GameObject activationPanel;
    [SerializeField] Text activityInformation_Text;
    [SerializeField] Image activityPercentage_Image;
    [SerializeField] Button confirmationButton;
    [SerializeField] Text confirmationText;
    
    double activityCompletedPercent;
    bool activityInitialized;

    // Start is called before the first frame update
    private void Start()
    {
        if (SaveObject.Instance.dailyActivityCompleted)
        {
            changeConfirmationButtonState(false);
        }
        else
        {
            changeConfirmationButtonState(true);
        }
    }

    void OnEnable()
    {
        if (SaveObject.Instance.activityRunning)
        {
            changePanelToInformation();
        }
    }

    public void StartActivity()
    {
        //Se inicia el servicio finalmente
        SaveObject.Instance.activityRunning = true;
        podometerManager.StartService(this.gameObject);

        changePanelToInformation();

        //Se oculta el menu de la actividad
        downHud_Controller.ShowAndHideActionsMenu(downHud_Controller.transform.GetChild(0).GetChild(0).gameObject);
    }

    public void StopActivity()
    {
        SaveObject.Instance.activityRunning = false;
        //Se tienen que sumar los puntos correspondientes a la actividad realizada
        //En caso de que no la haya cumplido del todo se deben almacenar los pasos para un futuro retomarlos
        podometerManager.giveActivityRewardToUser();
        changePanelToStart();
    }

    void changePanelToInformation()
    {
        //Se oculta el panel de confirmación de actividad y se sustituye por el de "in-activity"
        activationPanel.transform.GetChild(0).gameObject.SetActive(false);
        activationPanel.transform.GetChild(1).gameObject.SetActive(true);
    }

    void changePanelToStart()
    {
        //Se resetea el panel de activación
        activationPanel.transform.GetChild(1).gameObject.SetActive(false);
        activationPanel.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void setInformationOnPanel(double distanceRunned, double distanceToRun = 600)
    {
        //Actualiza la información de la interfaz
        activityCompletedPercent = distanceRunned / distanceToRun;

        activityInformation_Text.text = "Distancia recorrida:\n\n" + distanceRunned + " Pasos / " + distanceToRun + " Pasos";
        activityPercentage_Image.fillAmount = (float)activityCompletedPercent;
    }

    public void changeConfirmationButtonState(bool newState)
    {
        confirmationButton.interactable = newState;

        if(!newState)
        {
            //Cambiar texto
            confirmationText.text = "Ya se ha completado la actividad diaria. Vuelve mañana a por más.";
        }
    }

    public void setGoalReachedOnPanel()
    {
        activityInformation_Text.text = "Actividad completada, ¡Recoge tu recompensa!";
        changeConfirmationButtonState(false);
    }
}
