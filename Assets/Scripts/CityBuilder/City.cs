using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public float Energy { get; set; }
    public int Materials { get; set; }
    public float powerR = 0;
    public float activationValue = 0;
    public float powerRLastCheckPoint = 0;
    public CanvasController canvasController;

    private const int DAILY_STEPS = 3000;
    private const float SOLVED_CONFLICT_VALUE = 1f; // calculate depending on cards number ELIMINAR NO ES NECESARIO?


    private int population; //ELIMINAR?
    private int questionaryDone = 0;
    private int libraryAccess = 0;
    private int currentSteps = 0;
    private bool fullActivation = false;
    private float esencias = 0; //ELIMINAR?
    public TimeSpan inactiveTime;

    void Start()
    {
        Materials = SaveObject.Instance.materials;
        powerR = SaveObject.Instance.powerR;
        Energy = SaveObject.Instance.energy;

        InitializeCity();
    }

    public void InitializeCity()
    {
        if (SaveObject.Instance.date != "")
            CheckInactiveTime(SaveObject.Instance.date);
        else
        { // Nuevo juego
            increaseActivationValue(10);
            LanzarCuestionarioAlUsuario();
        }
        CheckAvailableBuildings();

    }

    private void UpdateTopHUD(bool updateEnergy, bool updatePowerR)
    {
        canvasController.updateSlidersValue(updateEnergy, updatePowerR);
        //updateMaterialsOnCanvas();
    }

    private void CheckInactiveTime(string lastAccess)
    {
        DateTime.TryParse(lastAccess, out DateTime lA);
        inactiveTime = DateTime.Now.Subtract(lA);

        //Nueva versión, sin sustituir lo de arriba
        DateTime today = DateTime.Now.Date;

        if(lA.Date != today.Date) // Día diferente
        {
            //Se permite al usuario volver a recoger energía
            SaveObject.Instance.dailyActivityCompleted = false;
            SaveObject.Instance.dailyQuestions_Done = false;
            SaveObject.Instance.enterInLibraryToday = false;
            SaveObject.Instance.actualSessionSteps = 0;
            SaveObject.Instance.dailyCompletedSteps = 0;
            GameManager.Instance.resetActivityNotifications();
            increaseActivationValue(-100);
            increaseActivationValue(10);
            if (inactiveTime.Days > 1)
            {
                //Aplicar penalización por no haber iniciado la aplicación
                ApplyPenalization(inactiveTime.Days);
            }
        }
        else //mismo día
        {
            // Lanzar cuestionario solo cuando pase un determinado tiempo
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


    public void CalculatePopulation (Building newBuilding)
    {
        //population += newBuilding.nLocals;
    }

    #region Activation

    public void CalculateActivation()
    {
        if (!fullActivation)
        {
            Energy = 10 + 10 * questionaryDone + 10 * libraryAccess + 0.7f * activationValue;
            CalculatePowerR();
            if (Energy >= 100)
            {
                Energy = 100;
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
        //energy = 100 * currentSteps / DAILY_STEPS;
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
                if (b.energyRequired <= Energy && b.energyRequired >= 0)
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
        //powerR = powerRLastCheckPoint + esencias * (1 + Energy / 100); // taking to account penalization
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

    public void UpdateMaterialsWithAnimation(int increment)
    {
        canvasController.topHUD.GetComponent<TOP_Hud_Controller>().updateMaterials_Text(Materials);
        if (increment != 0)
        {
            canvasController.topHUD.GetComponent<TOP_Hud_Controller>().increaseMaterialsOnCanvas(increment);
        }
    }

    private void UpdateMaterialsText()
    {
        canvasController.topHUD.GetComponent<TOP_Hud_Controller>().updateMaterials_Text(Materials);
    }

    public void increaseActivationValue(float addition)
    {
        Energy += addition;

        if(Energy < 0)
        {
            Energy = 0;
        }
        SaveObject.Instance.energy = Energy;

        UpdateTopHUD(true, false);
        CheckAvailableBuildings();
    }
}
