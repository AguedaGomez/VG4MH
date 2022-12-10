using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public float Activation { get; set; }
    public int Materials { get; set; }
    public float powerR = 0;
    public float activationValue = 0;
    public float powerRLastCheckPoint = 0;
    public List<string> availableBuildingsId = new List<string>(); // only those available (depending on the activation)
    public CanvasController canvasController;
    //public GameObject dailyQuestionsPanel_Prefab;
    //public GameObject dailyQuestion_ConfirmationPanel;

    private const int DAILY_STEPS = 3000;
    private const float SOLVED_CONFLICT_VALUE = 1f; // calculate depending on cards number
    //public?
    private int population;
    private int questionaryDone = 0;
    private int libraryAccess = 0;
    private int currentSteps = 0;
    private bool fullActivation = false;
    private float esencias = 0;
    public TimeSpan inactiveTime;

    void Start()
    {
        Materials = SaveObject.Instance.materials;
        powerR = SaveObject.Instance.powerR;
        activationValue = SaveObject.Instance.activationValue;

        InitializeCity(); //esto creo que no funcionará
    }

    //public void InitializeCity(string lastAccess)
    //{
    //    if (lastAccess != "")
    //        CheckInactiveTime(lastAccess);
    //    CalculateActivation(); //Also call to calclulatepowerR
    //}

    public void InitializeCity()
    {
        //Debug.Log("En initializeCity");
        //Debug.Log("fecha guardada: " + SaveObject.Instance.date);
        if (SaveObject.Instance.date != "")
            CheckInactiveTime(SaveObject.Instance.date);
        CalculateActivation(); //Also call to calclulatepowerR
                               //Actualizar los materiales de los edificios
        UpdateTopHUD();

        LanzarCuestionarioAlUsuario();
    }

    private void UpdateTopHUD()
    {
        canvasController.updateSlidersValue(activationValue, powerR);
        updateMaterialsOnCanvas();
    }

    private void CheckInactiveTime(string lastAccess)
    {
        //Debug.Log("City lastAccess: " + lastAccess);
        //Debug.Log("Tryparse " + DateTime.TryParse(lastAccess, out DateTime r) + r);
        DateTime.TryParse(lastAccess, out DateTime lA);
        inactiveTime = DateTime.Now.Subtract(lA);
        /*Debug.Log("City>CheckInactiveTime Inactive time in seconds: " + inactiveTime.TotalSeconds);
        ApplyPenalization(inactiveTime.Days);*/

        //Nueva versión, sin sustituir lo de arriba
        DateTime today = DateTime.Now.Date;

        if(lA.Date != today.Date)
        {
            //Día diferente
            //Se permite al usuario volver a hacer actividad
            SaveObject.Instance.dailyActivityCompleted = false;
            SaveObject.Instance.dailyQuestions_Done = false;
            SaveObject.Instance.enterInLibraryToday = false;
            SaveObject.Instance.actualSessionSteps = 0;
            SaveObject.Instance.dailyCompletedSteps = 0;
            GameManager.Instance.resetActivityNotifications();
            increaseActivationValue(-100);
            
            if(inactiveTime.Days > 1)
            {
                //Aplicar penalización por no haber iniciado la aplicación
                ApplyPenalization(inactiveTime.Days);
            }
        }

    }

    private void LanzarCuestionarioAlUsuario()
    {
        if(SaveObject.Instance.dailyQuestions_Done == false)
        {
            canvasController.ShowQuestionnaireConfirmationMssg();
            //dailyQuestion_ConfirmationPanel.SetActive(true);
        }
    }

    //public void dailyQuestionsConfirmation_Yes()
    //{
    //    Instantiate(dailyQuestionsPanel_Prefab, canvasController.transform);
    //    Destroy(dailyQuestion_ConfirmationPanel);
    //}

    //public void dailyQuestionsConfirmation_No()
    //{
    //    Destroy(dailyQuestion_ConfirmationPanel);
    //}

    public void CalculatePopulation (Building newBuilding)
    {
        //population += newBuilding.nLocals;
    }

    #region Activation

    public void CalculateActivation()
    {
        if (!fullActivation)
        {
            Activation = 10 + 10 * questionaryDone + 10 * libraryAccess + 0.7f * activationValue;
            CalculatePowerR();
            if (Activation >= 100)
            {
                Activation = 100;
                fullActivation = true;
            }
            CheckAvailableBuildings();
        }
       
    }

    public void FinishQuestionary(bool toggleState)
    {
        questionaryDone = Convert.ToInt32(toggleState);
        CalculateActivation();
    }

    public void VisitLibrary(bool toggleState)
    {
        libraryAccess = Convert.ToInt32(toggleState);
        CalculateActivation();
    }
    public void CalculatePhysicalActivity()
    {
        //activationValue = 100 * currentSteps / DAILY_STEPS;
        print("activación por los pasos dados: " + currentSteps);
        CalculateActivation();
    }
    public void SaveCurrentSteps(int nSteps)
    {
        if (DAILY_STEPS - nSteps >= 0)
            currentSteps += nSteps;
        else
            currentSteps = DAILY_STEPS;

        CalculatePhysicalActivity();
    }
    #endregion

    private void CheckAvailableBuildings()
    {
        foreach (var b in GameManager.Instance.buildingsInGameList)
        {
            if (b.buildingName!="Biblioteca")
            {
                if (b.energyRequired <= Activation && b.energyRequired >= 0)
                {
                    canvasController.UnlockBuilding(b.id);
                }
            }


        }
        
    }

    private int BaseMaterials()
    {
        return population;
    }

    public void CalculatePowerR()
    {
        //powerR = powerRLastCheckPoint + esencias * (1 + Activation / 100); // taking to account penalization
    }

    private void ApplyPenalization(int inactiveDays)
    {
        //powerRLastCheckPoint
    }

    public void SolveConflict()
    {
        esencias += SOLVED_CONFLICT_VALUE;
        CalculatePowerR();
    }

    public void DecreaseMaterials(int buildingPrice)
    {
        Materials -= buildingPrice;
        SaveObject.Instance.materials = Materials;
    }

    private int CalculatePowerRFactor()
    {
        if (powerR < 25)
            return 1;

        else if (powerR > 25 && powerR <= 50)
            return 2;

        else if (powerR > 50 && powerR <= 75)
            return 4;

        else if (powerR <= 100)
            return 8;
        else
            return 0;
    }

    public void updateMaterialsOnCanvas(int increment = 0)
    {
        canvasController.topHUD.GetComponent<TOP_Hud_Controller>().updateMaterials_Text(Materials);
        if(increment != 0)
        {
            canvasController.topHUD.GetComponent<TOP_Hud_Controller>().increaseMaterialsOnCanvas(increment);
        }
    }

    public void increaseActivationValue(float addition)
    {
        activationValue += addition;

        if(activationValue < 0)
        {
            activationValue = 0;
        }
        SaveObject.Instance.activationValue = activationValue;

        UpdateTopHUD();
    }
}
