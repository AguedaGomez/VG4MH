using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    // Fill with material per second data, population, building and progress

    public float Activation { get; set; }
    public int Materials { get; set; }
    public int powerR = 0;

    private const int MIN_MATERIALS = 2;
    private const float SECONDS = 15;
    private const int DAILY_STEPS = 3000;
    private int materialsPerSecond = 0;
    private int population;
    private int questionaryDone = 0;
    private int libraryAccess = 0;
    private int activationValue = 0;
    private int currentSteps = 0;
    private bool fullActivation = false;



    void Start()
    {
        StartCoroutine(CalculateMaterialsPerSecond());
        CalculateActivation();
    }


    public void CalculatePopulation (Building newBuilding)
    {
        population += newBuilding.nLocals;
    }

    public void CalculateActivation()
    {
        if (!fullActivation)
        {
            Activation = 10 + 10 * questionaryDone + 10 * libraryAccess + 0.7f * activationValue;
            if (Activation >= 100)
            {
                Activation = 100;
                fullActivation = true;
            }
        }
        
        print("valor activación = " + Activation);
    }

    public void FinishQuestionary()
    {
        questionaryDone = 1;
        CalculateActivation();
    }

    public void VisitLibrary()
    {
        libraryAccess = 1;
        CalculateActivation();
    }

    public void IncreasePowerR()
    {
        if (powerR <= 100)
            powerR += 10;
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

    IEnumerator CalculateMaterialsPerSecond()
    {
        while(true)
        {
            materialsPerSecond += MIN_MATERIALS * CalculatePowerRFactor();
            Materials = BaseMaterials() + materialsPerSecond;

            yield return new WaitForSeconds(SECONDS);
        }
        
    }

    private int BaseMaterials()
    {
        return population;
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
