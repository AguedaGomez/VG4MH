using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public Text indicatorText;
    public int maxMaterials = 40;
    public Button materialsButton;

    public int id;
    public int cost;
    public string buildingName;
    public int nLocals;
    public int activationRequired;
    public int row;
    public int col;

    public int materialsPerSecond = 0;
    private const int MIN_MATERIALS = 2;
    private const int SECONDS = 216;
    private int timeOfGeneration = SECONDS;
    private const string cityTag = "city";
    private const string INDICATOR_BEGINNING = "materiales: ";
    private GameObject city;
    private City cityScript;

    void Start()
    {
        city = GameObject.FindGameObjectWithTag(cityTag);
        cityScript = city.GetComponent<City>();
        timeOfGeneration = SECONDS;
        //StartCoroutine(CalculateMaterialsPerSecond());
    }
    public void InitializedAsDefault()
    {
        materialsPerSecond = 0;
        indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;
        timeOfGeneration = SECONDS;
        int r = UnityEngine.Random.Range(0, 8);
        StartCoroutine(WaitingTimeToGenerateMaterials(r));
    }
    public void SetCurrentMaterials(int materials, double inactiveTime) //when there are a saved game
    {   if (inactiveTime >= timeOfGeneration)
        {
            var d = Convert.ToInt32(Math.Round(inactiveTime / timeOfGeneration));
            materialsPerSecond = ( d * MIN_MATERIALS + materials);
            if (materialsPerSecond >= maxMaterials)
                materialsPerSecond = maxMaterials;
            materialsButton.interactable = true;
            StartCoroutine(CalculateMaterialsPerSecond());
        }
            
        else
        {
            materialsPerSecond = materials;
            indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;
            StartCoroutine(WaitingTimeToGenerateMaterials(Convert.ToInt32(timeOfGeneration - Math.Round(inactiveTime))));
        }
        
    }

    IEnumerator WaitingTimeToGenerateMaterials(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        materialsButton.interactable = true;
        StartCoroutine(CalculateMaterialsPerSecond());
    }
    IEnumerator CalculateMaterialsPerSecond()
    {
        
        while (true)
        {
            if (materialsPerSecond < maxMaterials)
            {
                materialsPerSecond += MIN_MATERIALS; //* CalculatePowerRFactor();
            }
            else
            {
                indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;
                yield break;
            }
            //Debug.Log("en building script: " + collectedMaterials);  
            indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;
            yield return new WaitForSeconds(timeOfGeneration);
        }

    }

    public void PickMaterials()
    {
        cityScript.Materials += materialsPerSecond;
        materialsPerSecond = 0;
        materialsButton.interactable = false;
        indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;

        StartCoroutine(WaitingTimeToGenerateMaterials(timeOfGeneration));
    }
}
