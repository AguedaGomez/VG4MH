using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public float Activation { get; set; }
    public int Materials { get; set; }
    public float powerR = 0;
    public float powerRLastCheckPoint = 0;
    public List<GameObject> buildingsInGame = new List<GameObject>(); // Gameobject for Addbuilding in board (the model is needed)
    public Dictionary<string, GameObject> availableBuildings = new Dictionary<string, GameObject>();

    private const int DAILY_STEPS = 3000;
    private const float SOLVED_CONFLICT_VALUE = 1f; // calculate depending on cards number
    //public?
    private int population;
    private int questionaryDone = 0;
    private int libraryAccess = 0;
    private int activationValue = 0;
    private int currentSteps = 0;
    private bool fullActivation = false;
    private float esencias = 0;
    public TimeSpan inactiveTime;

    void Start()
    {
        Materials = SaveObject.Instance.materials;
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
        Debug.Log("En initializeCity");
        Debug.Log("fecha guardada: " + SaveObject.Instance.date);
        if (SaveObject.Instance.date != "")
            CheckInactiveTime(SaveObject.Instance.date);
        CalculateActivation(); //Also call to calclulatepowerR
    }

    private void CheckInactiveTime(string lastAccess)
    {
        Debug.Log("City lastAccess: " + lastAccess);
        Debug.Log("Tryparse " + DateTime.TryParse(lastAccess, out DateTime r) + r);
        //DateTime lA = DateTime.Parse(lastAccess);
        DateTime.TryParse(lastAccess, out DateTime lA);
        inactiveTime = DateTime.Now.Subtract(lA);
        //Debug.Log("al pasar a datetime " + lA);
        //string nowS = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        //DateTime now = DateTime.Parse(nowS);
        //Debug.Log("ahora es " + now);
        //// changa to local variable
        //inactiveTime = now.Subtract(lA);
        Debug.Log("City>CheckInactiveTime Inactive time in seconds: " + inactiveTime.TotalSeconds);
        ApplyPenalization(inactiveTime.Days);
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

    private void CheckAvailableBuildings()
    {
        foreach (var b in buildingsInGame)
        {
            Building building = b.GetComponent<Building>();
            if (building.activationRequired <= Activation)
            {
                if (availableBuildings.ContainsKey(building.buildingName) == false)
                {
                    availableBuildings.Add(building.buildingName, b);
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
        powerR = powerRLastCheckPoint + esencias * (1 + Activation / 100); // taking to account penalization
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

}
