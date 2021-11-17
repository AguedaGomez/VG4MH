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
    private Vector3 currentColliderPosition;
    private GameObject selectedBuilding;
    private Building selectedBuildingScript;

    private void OnEnable()
    {
        GridUI.SetActive(false);
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
                        //Debug.Log("interactua con camara");
                        cameraController.moveCamera = true;
                        cameraController.Status = CityBuilderResources.Status.Game;
                    }
                    else if (CheckInteractionWith(GHOST_TAG))
                    {
                        if (CheckDoubleTap())
                        {
                            //Debug.Log("doble tap");
                            AddBuilding();
                        }
                        else
                        {
                            //Debug.Log("interactua con ghost");
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
                            if (!board.CheckBoardLimits(gridPosition, selectedBuildingScript))
                            {
                                currentCollider.gameObject.transform.position = gridPosition;
                                currentColliderPosition = currentCollider.gameObject.transform.position;
                                CheckInBoard();
                            }
                            
                        }
                    }

                    break;
                case TouchPhase.Ended:
                    //if (CheckInteractionWith(GHOST_TAG))
                    //    CheckInBoard();
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
        //Debug.Log("check in board");
        bool availability = board.CheckSpaceAtPosition(selectedBuilding, currentColliderPosition);
        board.ChangeBuildingColor(availability);
    }
    void InteractWithBoard()
    {
        if (board.CheckSpaceAtPosition(selectedBuilding, currentColliderPosition))
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
            selectedBuildingScript = selectedBuilding.GetComponent<Building>();
            AddBuildingInEditMode();
            GridUI.SetActive(true);
        }
    }

    private void AddBuildingInEditMode()
    {
        //if(currentCollider!=null) Destroy(currentCollider.gameObject);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f)) {
            Vector3 gridPosition = board.CalculateGridPosition(hit.point);
            
            board.AddBuildingInEditMode(selectedBuilding, board.CalculateGridPosition(hit.point));

            currentColliderPosition = gridPosition;
        }
            
    }

    private void AddBuilding()
    {
        board.AddBuilding(selectedBuilding, currentColliderPosition, -1);
        Destroy(currentCollider.gameObject);
        GridUI.SetActive(false);
        cameraController.Status = CityBuilderResources.Status.Game;
    }


}
