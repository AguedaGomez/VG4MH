﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingController : MonoBehaviour
{
    public City city; //mirar cambiar city de aqui
    public Board board;
    private GameObject selectedBuilding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   void Update()
    {
        if( Input.GetMouseButtonDown(0) && selectedBuilding != null)
        {
            InteractWithBoard();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            CheckInteractionWithLocal();
        }
    }

    private void CheckInteractionWithLocal()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // change for mobile
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    if (hit.collider.tag == "local")
        //    {
        //        hit.collider.gameObject.GetComponent<LocalsMessages>().GenerateGeneralMessage();
        //    }
        //}
    }

    void InteractWithBoard()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // change for mobile
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 gridPosition = board.CalculateGridPosition(hit.point);
            if (board.CheckForBuildingAtPosition(gridPosition))
            {
                Building buildingScript = selectedBuilding.GetComponent<Building>();

                if (buildingScript.cost <= city.Materials)
                {
                    board.AddBuilding(selectedBuilding, gridPosition, -1);

                    city.CalculatePopulation(buildingScript);
                    city.DecreaseMaterials(buildingScript.cost);
                }
                selectedBuilding = null;
            }
        }
    }
    public void EnableBuilder(string buildingName)
    {
        if (city.availableBuildings.ContainsKey(buildingName))
        {
            selectedBuilding = city.availableBuildings[buildingName];
        }
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

}
