using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialGeneratorBuilding : Building
{
    public Text indicatorText;
    public int maxMaterials = 40;
    public Button materialsButton;
    //public int materialsPerSecond = 0; in parent due to board CHANGE
   
    private const int MIN_MATERIALS = 1;
    private const int SECONDS = 2;
    private int timeOfGeneration = SECONDS;
    private const string cityTag = "city";
    private const string INDICATOR_BEGINNING = "";
    private GameObject city; //canvas controller?
    private City cityScript;
    private Camera camViewport;
    void Start()
    {
        city = GameObject.FindGameObjectWithTag(cityTag);
        cityScript = city.GetComponent<City>();
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
    {
        if (inactiveTime >= timeOfGeneration)
        {
            var d = Convert.ToInt32(Math.Round(inactiveTime / timeOfGeneration));
            //Debug.Log("tiempo inactivo: " + inactiveTime);
            //Debug.Log("tiempo inactivo entre tiempo de generación: " + d);
            materialsPerSecond = (d * MIN_MATERIALS + materials);
            if (materialsPerSecond >= maxMaterials)
                materialsPerSecond = maxMaterials;
            materialsButton.interactable = true;
            StartCoroutine(CalculateMaterialsPerSecond());
        }

        else
        {
            //Debug.Log("el tiempo inactivo es menos que el tiempo de generación");
            materialsPerSecond = materials;
            //Debug.Log("materialsPerSecond " + materialsPerSecond);
            indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;

            if (materialsPerSecond > 0)
            {
                materialsButton.interactable = true;
                //Debug.Log("Tiempo que ha pasado: " + Convert.ToInt32(timeOfGeneration - Math.Round(inactiveTime)));
            }
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
                UpdateMaterialsInSaveObject();
                yield break;
            }
            //Debug.Log("en building script: " + collectedMaterials);  
            indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;
            UpdateMaterialsInSaveObject();
            yield return new WaitForSeconds(timeOfGeneration);
        }

    }

    public void UpdateMaterialsInSaveObject()
    {
        //Debug.Log("Actualizando materiales del objeto " + id + " " + SaveObject.Instance.buildingsInBoard.Find(n => n.buildingName == buildingName).currentMaterials);

        SaveObject.Instance.buildingsInBoard.Find(n => n.id == id).currentMaterials = materialsPerSecond;
    }

    public void PickMaterials()
    {
        int incremental = materialsPerSecond;
        cityScript.Materials += materialsPerSecond;
        SaveObject.Instance.materials = cityScript.Materials;
        cityScript.updateMaterialsOnCanvas(incremental);
        materialsPerSecond = 0;
        UpdateMaterialsInSaveObject();
        materialsButton.interactable = false;
        indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;

        StartCoroutine(WaitingTimeToGenerateMaterials(timeOfGeneration));
    }

    
}
