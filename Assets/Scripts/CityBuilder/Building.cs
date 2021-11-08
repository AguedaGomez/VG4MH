﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    //public Text indicatorText;
    //public int maxMaterials = 40;
    //public Button materialsButton;

    public string id; // id when building GO is created (string composed xz coordinates)
    public int cost; // materials needed to built it
    public string buildingName;
    public int nLocals; // number of new inhabitants that attracks
    public int activationRequired; // minimun of activation to built it
    public int row; // row (z) in the board where it was built
    public int col; // col (x) in the board where it was built
    public int cellsInRow; // all the cells that it occupies in z
    public int cellsInCol; // all the cells that it occupies in x
    public int powerRIncrease; // percentage of powerR that increases when built

    public int materialsPerSecond = 0;

    public enum Type
    {
        MATERIALGENERATORBUILDING,
        NONE
    }
    public Type type; // if the building generates materials

    //public int materialsPerSecond = 0;
    //private const int MIN_MATERIALS = 2;
    //private const int SECONDS = 60;
    //private int timeOfGeneration = SECONDS;
    //private const string cityTag = "city";
    //private const string INDICATOR_BEGINNING = "materiales: ";
    //private GameObject city;
    //private City cityScript;

    //void Start()
    //{
    //    city = GameObject.FindGameObjectWithTag(cityTag);
    //    cityScript = city.GetComponent<City>();
    //    timeOfGeneration = SECONDS;
    //    //StartCoroutine(CalculateMaterialsPerSecond());
    //}

    //public void InitializedAsDefault()
    //{
    //    materialsPerSecond = 0;
    //    indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;
    //    timeOfGeneration = SECONDS;
    //    int r = UnityEngine.Random.Range(0, 8);
    //    StartCoroutine(WaitingTimeToGenerateMaterials(r));
    //}
    //public void SetCurrentMaterials(int materials, double inactiveTime) //when there are a saved game
    //{   if (inactiveTime >= timeOfGeneration)
    //    {
    //        var d = Convert.ToInt32(Math.Round(inactiveTime / timeOfGeneration));
    //        //Debug.Log("tiempo inactivo: " + inactiveTime);
    //        //Debug.Log("tiempo inactivo entre tiempo de generación: " + d);
    //        materialsPerSecond = ( d * MIN_MATERIALS + materials);
    //        if (materialsPerSecond >= maxMaterials)
    //            materialsPerSecond = maxMaterials;
    //        materialsButton.interactable = true;
    //        StartCoroutine(CalculateMaterialsPerSecond());
    //    }
            
    //    else
    //    {
    //        //Debug.Log("el tiempo inactivo es menos que el tiempo de generación");
    //        materialsPerSecond = materials;
    //        //Debug.Log("materialsPerSecond " + materialsPerSecond);
    //        indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;

    //        if (materialsPerSecond > 0)
    //        {
    //            materialsButton.interactable = true;
    //            //Debug.Log("Tiempo que ha pasado: " + Convert.ToInt32(timeOfGeneration - Math.Round(inactiveTime)));
    //        }
    //        StartCoroutine(WaitingTimeToGenerateMaterials(Convert.ToInt32(timeOfGeneration - Math.Round(inactiveTime))));

    //    }
        
    //}

    //IEnumerator WaitingTimeToGenerateMaterials(int seconds)
    //{
    //    yield return new WaitForSeconds(seconds);
    //    materialsButton.interactable = true;
    //    StartCoroutine(CalculateMaterialsPerSecond());
    //}
    //IEnumerator CalculateMaterialsPerSecond()
    //{
        
    //    while (true)
    //    {
    //        if (materialsPerSecond < maxMaterials)
    //        {
    //            materialsPerSecond += MIN_MATERIALS; //* CalculatePowerRFactor();
    //        }
    //        else
    //        {
    //            indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;
    //            UpdateMaterialsInSaveObject();
    //            yield break;
    //        }
    //        //Debug.Log("en building script: " + collectedMaterials);  
    //        indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;
    //        UpdateMaterialsInSaveObject();
    //        yield return new WaitForSeconds(timeOfGeneration);
    //    }

    //}

    //public void UpdateMaterialsInSaveObject()
    //{
    //    SaveObject.Instance.boardState.Find(n => n.id == id).currentMaterials = materialsPerSecond;
    //    //Debug.Log("Actualizando materiales del objeto " + id + " " + SaveObject.Instance.boardState.Find(n => n.buildingName == buildingName).currentMaterials);
    //}

    //public void PickMaterials()
    //{
    //    cityScript.Materials += materialsPerSecond;
    //    SaveObject.Instance.materials = cityScript.Materials;
    //    materialsPerSecond = 0;
    //    UpdateMaterialsInSaveObject();
    //    materialsButton.interactable = false;
    //    indicatorText.text = INDICATOR_BEGINNING + materialsPerSecond + "/" + maxMaterials;

    //    StartCoroutine(WaitingTimeToGenerateMaterials(timeOfGeneration));
    //}
}
