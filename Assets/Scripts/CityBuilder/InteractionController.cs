using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private const string GHOST_TAG = "ghost";
    private const string BOARD_TAG = "board";

    public City city; //mirar cambiar city de aqui
    public Board board;
    
    public CameraController cameraController;
    [SerializeField] private GameObject GridUI;

    private Collider currentCollider;
    private GameObject selectedBuilding;

    private void OnEnable()
    {
        //GridUI.SetActive(false);
    }
    void Start()
    {
        
    }

   void Update()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    if (CheckInteractionWith(BOARD_TAG))
                    {
                        Debug.Log("interactua con camara");
                        cameraController.moveCamera = true;
                        cameraController.Status = CityBuilderResources.Status.Game;
                    }
                    else if (CheckInteractionWith(GHOST_TAG))
                    {
                        if (CheckDoubleTap())
                        {
                            Debug.Log("doble tap");
                            InteractWithBoard();
                        }
                        else
                        {
                            cameraController.Status = CityBuilderResources.Status.Build;
                        }

                    }
                    break;
                case TouchPhase.Moved:
                    if (currentCollider.tag == GHOST_TAG)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Vector3 gridPosition = board.CalculateGridPosition(hit.point);
                            currentCollider.gameObject.transform.position = gridPosition;
                        }
                    }

                    break;
                case TouchPhase.Ended:
                    if (CheckInteractionWith(GHOST_TAG))
                        CheckInBoard();
                    //currentCollider = null;
                    break;
                default:
                    break;
            }

        }
    }

    private bool CheckDoubleTap()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.tapCount == 2)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckInteractionWith(string tag)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // change for mobile
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentCollider = hit.collider;
            return (hit.collider.tag == tag);
        }
        return false;
    }

    void CheckInBoard()
    {
        var currentBPosition = currentCollider.gameObject.transform.position;
        board.CheckSpaceAtPosition(selectedBuilding, currentBPosition);
    }
    void InteractWithBoard()
    {
        var currentBPosition = currentCollider.gameObject.transform.position;
        
        if (board.CheckSpaceAtPosition(selectedBuilding, currentBPosition))
        {
            Building buildingScript = selectedBuilding.GetComponent<Building>();
            city.CalculatePopulation(buildingScript);
            city.DecreaseMaterials(buildingScript.cost);
        }
        selectedBuilding = null;

        //Ray ray = Camera.main.ScreenPointToRay(finalPoint); // change for mobile aquí había un Input.mousePosition
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Debug.Log("interactuando con el board");
        //    Vector3 gridPosition = board.CalculateGridPosition(finalPoint); //aqui hit.point
        //    if (board.CheckForBuildingAtPosition(gridPosition))
        //    {
        //        Building buildingScript = selectedBuilding.GetComponent<Building>();

        //        if (buildingScript.cost <= city.Materials)
        //        {
        //            //board.AddBuilding(selectedBuilding, gridPosition, -1);
        //            board.AddBuildingInEditMode(selectedBuilding, gridPosition);

        //            city.CalculatePopulation(buildingScript);
        //            city.DecreaseMaterials(buildingScript.cost);
        //        }
        //        selectedBuilding = null;
        //    }
        //}

    }
    public void EnableBuilder(string buildingName)
    {
        if (city.availableBuildings.ContainsKey(buildingName))
        {
            selectedBuilding = city.availableBuildings[buildingName];
            AddBuildingInEditMode();
            GridUI.SetActive(true);
        }
    }

    private void AddBuildingInEditMode()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f)) { }
            board.AddBuildingInEditMode(selectedBuilding, board.CalculateGridPosition(hit.point));
    }


}
