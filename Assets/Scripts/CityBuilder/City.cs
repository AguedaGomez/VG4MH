using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    // Fill with material per second data, population, building and progress
    // use properties?

    public float Activation { get; set; }
    public int Materials { get; set; }
    public int powerR = 0;

    private const int MIN_MATERIALS = 2;
    private float seconds = 2f;
    private int materialsPerSecond = 0;
    private int population;

    void Start()
    {
        StartCoroutine(CalculateMaterialsPerSecond()); 
    }


    public void CalculatePopulation (Building newBuilding)
    {
        population += newBuilding.nLocals;


    }

    public void CalculateActivation()
    {
        Activation = 100;
    }

    public void IncreasePowerR()
    {
        if (powerR <= 100)
            powerR += 10;
    }

    IEnumerator CalculateMaterialsPerSecond()
    {
        while(true)
        {
            materialsPerSecond += MIN_MATERIALS * CalculatePowerRFactor();
            Materials = BaseMaterials() + materialsPerSecond;

            yield return new WaitForSeconds(1f);
        }
        
    }

    private int BaseMaterials()
    {
        return population;
    }

    private int CalculatePowerRFactor()
    {
        if (powerR < 25)
        {
            seconds = 2.5f;
            return 1;
        }
        else if (powerR > 25 && powerR <= 50)
        {
            seconds = 2f;
            return 2;
        }
        else if (powerR > 50 && powerR <= 75)
        {
            seconds = 1f;
            return 4;
        }
        else if (powerR <= 100)
        {
            seconds = 0.5f;
            return 8;
        }   
        else
            return 0;        
    }
}
