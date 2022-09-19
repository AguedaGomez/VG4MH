using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivityService : MonoBehaviour
{
    private AndroidJavaClass unityClass;
    private AndroidJavaObject unityActivity;
    private AndroidJavaClass customClass;
    private const string PlayerPrefsTotalSteps = "totalSteps";
    private const string PackageName = "com.kdg.toast.plugin.Bridge";
    private const string UnityDefaultJavaClassName = "com.unity3d.player.UnityPlayer";
    private const string CustomClassReceiveActivityInstanceMethod = "ReceiveActivityInstance";
    private const string CustomClassStartServiceMethod = "StartService";
    private const string CustomClassStopServiceMethod = "StopService";
    private const string CustomClassGetCurrentStepsMethod = "GetCurrentSteps";
    private const string CustomClassIsServiceRunning = "isServiceActive";
    private const string CustomClassSyncDataMethod = "SyncData";

    public int actualSteps = 0;
    public int stepsInThisSession = 0;
    int stepsToGo = 600;
    [SerializeField]GameObject activityDecorator;
    [SerializeField]TOP_Hud_Controller topHud_Controller;
    [SerializeField] Notification startActivityNotification;

    private void Awake()
    {
        SendActivityReference(PackageName);
    }

    private void Start()
    {
        if (SaveObject.Instance.activityRunning)
        {
            GetCurrentSteps(true);
            StartService();
            //StartCoroutine(updateCanvasStepData());
        }
    }

    private void SendActivityReference(string packageName)
    {
        unityClass = new AndroidJavaClass(UnityDefaultJavaClassName);
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        customClass = new AndroidJavaClass(packageName);
        customClass.CallStatic(CustomClassReceiveActivityInstanceMethod, unityActivity);
    }

    public void StartService(GameObject activityPanel = null)
    {
        if(activityPanel != null)
        {
            activityDecorator = activityPanel;
        }
        PopUp_Manager.Instance.addNotificationToQueue(startActivityNotification);
        //Se recogen los posibles pasos que se hayan dado mientras la aplicación estaba en suspensión y se suma
        GetCurrentSteps();
        actualSteps = SaveObject.Instance.actualSessionSteps + stepsInThisSession;

        Debug.Log("Start Service");
        customClass.CallStatic(CustomClassStartServiceMethod);

        //Se resetean los datos a 0 y se comienza de nuevo
        SyncData();
        StartCoroutine(updateCanvasStepData());
    }

    public void StopService()
    {
        customClass.CallStatic(CustomClassStopServiceMethod);
    }


    public void GetCurrentSteps(bool initialCountingSteps = false)
    {
        int stepsCount = customClass.CallStatic<int>(CustomClassGetCurrentStepsMethod);
        stepsInThisSession = stepsCount;
        //actualSteps = stepsCount;
    }

    public void SyncData()
    {
        var data = customClass.CallStatic<string>(CustomClassSyncDataMethod);

        var parsedData = data.Split('#');
        var dateOfSync = parsedData[0] + " - " + parsedData[1];
        //syncedDateText.text = dateOfSync;
        var receivedSteps = int.Parse(parsedData[2]);
        var prefsSteps = PlayerPrefs.GetInt(PlayerPrefsTotalSteps, 0);
        var prefsStepsToSave = prefsSteps + receivedSteps;
        PlayerPrefs.SetInt(PlayerPrefsTotalSteps, prefsStepsToSave);
        //totalStepsText.text = prefsStepsToSave.ToString();

        GetCurrentSteps();
    }

    public IEnumerator updateCanvasStepData()
    {
        ActivityPanel_Decorator activityPanel = activityDecorator.GetComponent<ActivityPanel_Decorator>();
        topHud_Controller.Start_IconShining(Card.Resource.ACTIVATION);

        //Corutina que se encarga de comprobar los pasos dados cada "x" tiempo de forma continua
        while (SaveObject.Instance.activityRunning)
        {
            yield return new WaitForSeconds(1.5f);

            //Se actualiza el valor desde el servicio y posteriormente se actualiza en el canvas
            GetCurrentSteps();
            checkIfGoalIsReached();
            saveActualStepsOnSavedata(actualSteps + stepsInThisSession);

            if (activityPanel.gameObject.active)
            {
                activityPanel.setInformationOnPanel(actualSteps + stepsInThisSession + SaveObject.Instance.dailyCompletedSteps);
            }

            //Comprobar si se ha llegado a la meta
            if ((actualSteps + stepsInThisSession + SaveObject.Instance.dailyCompletedSteps) >= stepsToGo)
            {
                //actualSteps = stepsToGo;
                SaveObject.Instance.dailyCompletedSteps = stepsToGo;
                SaveObject.Instance.activityRunning = false;
                SaveObject.Instance.dailyActivityCompleted = true;

                activityPanel.gameObject.SetActive(true);
                activityDecorator.GetComponent<ActivityPanel_Decorator>().setGoalReachedOnPanel();
            }
        }

        //Se acaba el ejercicio, bien sea por llegar a la meta o no
        //Si se sale de la aplicación, bien sea soft o hard, no se llega a este punto
        if(!SaveObject.Instance.dailyActivityCompleted)
        {
            actualSteps = actualSteps + stepsInThisSession;
        }

        SyncData();
        Debug.Log("StopCoroutine");
        topHud_Controller.Stop_IconShining(Card.Resource.ACTIVATION);
        //Se añade el valor correspondiente al recurso de "Activación"
        activityPanel.gameObject.SetActive(true);
        StopService();
    }

    public void saveActualStepsOnSavedata(int stepsToSave)
    {
        SaveObject.Instance.actualSessionSteps = stepsToSave;
    }

    void checkIfGoalIsReached()
    {
        if ((actualSteps + stepsInThisSession + SaveObject.Instance.dailyCompletedSteps) > GameManager.Instance.goalsToReach[0].stepsToShow)
        {
            if(!GameManager.Instance.goalsToReach[0].hasBeenShown)
            {
                GameManager.Instance.goalsToReach[0].hasBeenShown = true;
                PopUp_Manager.Instance.addNotificationToQueue(GameManager.Instance.goalsToReach[0]);
            }
            GameManager.Instance.goalsToReach.RemoveAt(0);
        }
    }

    public void giveActivityRewardToUser()
    {
        int activationReward = (int)(actualSteps / 10);
        SaveObject.Instance.dailyCompletedSteps += actualSteps;
        actualSteps = 0;
        SaveObject.Instance.actualSessionSteps = 0;

        //Debug.Log(activationReward);
        //GameManager.Instance.gameObject.GetComponent<RewardManager>().UpdateResource(Card.Resource.ACTIVATION, activationReward);
        FindObjectOfType<City>().increaseActivationValue(activationReward);
        topHud_Controller.Start_VisualResourceStatChange(Card.Resource.ACTIVATION, activationReward);
    }
}