using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    private const string GHOST_TAG = "ghost";
    private const string BOARD_TAG = "board";
    private const string USERINTERFACE_TAG = "ui";

    public City city; //mirar cambiar city de aqui
    public Board board;
    
    public GraphicRaycaster canvasGraphicRaycaster;
    public EventSystem eventSystem;
    
    public CameraController cameraController;
    [SerializeField] private GameObject GridUI;

    private GameObject currentBuilding;
    private Vector3 currentBuildingPosition;
    private Vector3 positionInCells;
    private GameObject selectedBuilding;
    private Building selectedBuildingScript;
    private int xCell, zCell;
    private float yCell = .5f;

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
                    
                    if (CheckInterationWithUI())
                        GameManager.Instance.interactingWithUI = true;
                    if (CheckInteractionWith(BOARD_TAG))
                    {

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
                    if (currentBuilding.tag == GHOST_TAG)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {

                            Vector3 gridPosition = board.CalculateGridPosition(hit.point, selectedBuildingScript.GetCellsX(), out xCell, out zCell);
                            
                            if (!board.CheckBoardLimits(gridPosition, selectedBuildingScript))
                            {
                                currentBuilding.transform.parent.position = gridPosition;
                                currentBuildingPosition = currentBuilding.transform.parent.position; //posición en grid fila, col
                                CheckInBoard();
                            }
                            
                        }
                    }

                    break;
                case TouchPhase.Ended:
                    if (!CheckInterationWithUI())
                        GameManager.Instance.interactingWithUI = false;
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
            currentBuilding = hit.collider.gameObject;
            //Debug.Log("Interactuando con: " + currentBuilding.name);
            return (hit.collider.tag == tag);
        }
        return false;
    }

    private bool CheckInterationWithUI()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        canvasGraphicRaycaster.Raycast(pointerEventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.tag == USERINTERFACE_TAG)
                return true;
        }
        return false;

    }

    void CheckInBoard()
    {
        //Debug.Log("check in board");
        bool availability = board.CheckSpaceAtPosition(selectedBuilding, xCell, zCell);
        board.ChangeBuildingColor(availability);
    }
    void InteractWithBoard()
    {
        if (board.CheckSpaceAtPosition(selectedBuilding, xCell, zCell))
        {
            Building buildingScript = selectedBuilding.GetComponent<Building>();
            city.CalculatePopulation(buildingScript);
            //city.DecreaseMaterials(buildingScript.cost);
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

    public void SaveBuildingToConstruct(Construction dataBuilding)
    {
        GameManager.Instance.buildingInConstruction = dataBuilding;
        selectedBuilding = GameManager.Instance.buildingInConstruction.prefab;
        selectedBuilding.GetComponent<Building>().InitializeBuildingPrefab(dataBuilding);
    }
    public void EnableBuilder()
    {
        selectedBuildingScript = selectedBuilding.GetComponent<Building>();
        AddBuildingInEditMode();
        GridUI.SetActive(true);

    }

    private void AddBuildingInEditMode()
    {
        //if(currentCollider!=null) Destroy(currentCollider.gameObject);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f)) {
            Vector3 gridPosition = board.CalculateGridPosition(hit.point, selectedBuildingScript.GetCellsX(), out xCell, out zCell);

            board.AddBuildingInEditMode(selectedBuilding, gridPosition, xCell, zCell);

            currentBuildingPosition = gridPosition;
        }
            
    }

    private void AddBuilding()
    {
        board.AddBuilding(GameManager.Instance.buildingInConstruction.prefab, currentBuildingPosition, -1,true, xCell, zCell);
        Destroy(currentBuilding.transform.parent.gameObject);
        GridUI.SetActive(false);
        GameManager.Instance.buildingInConstruction = null;
        cameraController.Status = CityBuilderResources.Status.Game;
    }


}
