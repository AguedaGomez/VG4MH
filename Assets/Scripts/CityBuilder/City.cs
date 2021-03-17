using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    // Fill with material per second data, population, building and progress

    public float Activation { get; set; }
    public int Materials { get; set; }
    public float powerR = 0;
    public List<GameObject> buildingsInGame = new List<GameObject>(); // Gameobject for Addbuilding in board (the model is needed)
    public Dictionary<string, GameObject> availableBuildigs = new Dictionary<string, GameObject>();

    private const int MIN_MATERIALS = 2;
    private const float SECONDS = 15;
    private const int DAILY_STEPS = 3000;
    private const float SOLVED_CONFLICT_VALUE = 1f; // calculate depending on cards number
    private int materialsPerSecond = 0;
    private int population;
    private int questionaryDone = 0;
    private int libraryAccess = 0;
    private int activationValue = 0;
    private int currentSteps = 0;
    private bool fullActivation = false;
    private float esencias = 0;



    void Start()
    {
        StartCoroutine(CalculateMaterialsPerSecond());
        CalculateActivation();
    }


    public void CalculatePopulation (Building newBuilding)
    {
        population += newBuilding.nLocals;
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
        
        print("valor activación = " + Activation);
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
        activationValue = 100 * currentSteps / DAILY_STEPS;
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

    IEnumerator CalculateMaterialsPerSecond()
    {
        while(true)
        {
            materialsPerSecond += MIN_MATERIALS * CalculatePowerRFactor();
            Materials = BaseMaterials() + materialsPerSecond;

            yield return new WaitForSeconds(SECONDS);
        }
        
    }
    private void CheckAvailableBuildings()
    {
        foreach (var b in buildingsInGame)
        {
            Building building = b.GetComponent<Building>();
            if (building.activationRequired <= Activation)
                if (!availableBuildigs.ContainsKey(building.buildingName))
                    availableBuildigs.Add(building.buildingName, b);
        }
        
    }

    private int BaseMaterials()
    {
        return population;
    }

    public void CalculatePowerR()
    {
        powerR = esencias * (1 + Activation / 100); // taking to account penalization
    }

    public void SolveConflict()
    {
        esencias += SOLVED_CONFLICT_VALUE;
        CalculatePowerR();
    }
    public void DecreaseMaterials(int buildingPrice)
    {
        Materials -= buildingPrice;
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
}
