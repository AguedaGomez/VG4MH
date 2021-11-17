using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : MonoBehaviour
{
    // TODO: configuracion inicial
    public GameObject staticBuildingA;
    public GameObject library;
    public City city; //Eliminar city de aquí?

    private float cellSize = 4f;
    private int numCells;
    private float boardHeight, boardWidth;
    private Building[,] buildings;
    private BoardView boardView;

    private bool[,] boardOccupationStatus;
    private bool occupiedCell = true;
    
    private string buildingsPath = "Prefabs/CityBuilder/Buildings";

    void Awake()
    {
        // creates grid

        boardWidth = transform.localScale.x;
        boardHeight = transform.localScale.z;

        numCells = (int)(boardWidth / cellSize);

        buildings = new Building[numCells, numCells];

        boardOccupationStatus = new bool[numCells, numCells];

        boardView = this.GetComponent<BoardView>();
  
    }

    void Start()
    {
        InitializeBoard();
    }

    public void AddBuildingInEditMode(GameObject building, Vector3 position)
    {
        bool availability = CheckSpaceAtPosition(building, position);
        ChangeBuildingColor(availability);
        GameObject currentBuilding = boardView.SetUpBuildingEditMode(building, availability, position);
    }

    public bool CheckSpaceAtPosition(GameObject building, Vector3 position)
    {
        int x = CalculateRowColumn(position.x);
        int z = CalculateRowColumn(position.z);
        Building buildingScript = building.GetComponent<Building>();

        //bool availableSpace = CheckAvailableSpace(x, z, buildingScript);
        //bool buildingAtPosition = CheckForBuildingAtPosition(position);
        return CheckAvailableSpace(x, z, buildingScript) && CheckForBuildingAtPosition(position);
       
    }

    public void ChangeBuildingColor(bool availability)
    {
        boardView.ColorDependingAvailability(availability);
    }
    public void AddBuilding(GameObject building, Vector3 position, int currentMaterials)
    {
        if (CheckSpaceAtPosition(building, position))
        {

            Transform buildingTransform = building.GetComponent<Transform>();
            Quaternion buildingRotation = buildingTransform.rotation;
            GameObject createdBuilding = Instantiate(building, position, buildingRotation);
            Building buildingScript = createdBuilding.GetComponent<Building>();
            createdBuilding.transform.name = buildingScript.buildingName;

            int x = CalculateRowColumn(position.x);
            int z = CalculateRowColumn(position.z);

            buildingScript.id = x + "" + z + "";

            if (buildingScript.type == Building.Type.MATERIALGENERATORBUILDING)
            {
                MaterialGeneratorBuilding mGBScript = createdBuilding.GetComponent<MaterialGeneratorBuilding>();
                buildings[x, z] = mGBScript;

                if (currentMaterials < 0)
                {
                    SaveBoardStateInList(x, z);
                    mGBScript.InitializedAsDefault();
                }
                    
                else
                {
                    SaveBoardStatus(x, z); //esto es asi?
                    mGBScript.SetCurrentMaterials(currentMaterials, Math.Floor(city.inactiveTime.TotalSeconds));
                }
                    
            } else
            {
                buildings[x, z] = buildingScript;
                
                if (currentMaterials < 0)
                    SaveBoardStateInList(x, z);
            }   
        }
        else
        {
            //mostrar mensaje de que no se puede construir ahi
        }

    }

    public bool CheckForBuildingAtPosition(Vector3 position)
    {
        return buildings[CalculateRowColumn(position.x), CalculateRowColumn(position.z)] == null;
    }

    private bool CheckAvailableSpace(int x, int z, Building currentBuilding)
    {
        for (int r = 0; r < currentBuilding.cellsInZ; r++)
        {
            for (int c = 0; c < currentBuilding.cellsInX; c++)
            {
                if (boardOccupationStatus[x + c, z - r] == occupiedCell) // there isn't available space
                    return false;
            }
        }
        return true;
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        int xCell = CalculateRowColumn(position.x);
        float yCell = .5f;
        int zCell = CalculateRowColumn(position.z);

        return new Vector3(xCell*cellSize, yCell, zCell*cellSize);
    }
    private Vector3 CalculatePosition(Vector3 gridPosition)
    {
        return new Vector3(gridPosition.x * cellSize, .5f, gridPosition.z * cellSize);
    }

    private int CalculateRowColumn(float cordPosition)
    {
        return Mathf.RoundToInt(cordPosition / cellSize); // number of cell
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (float x = 0; x < boardHeight; x += cellSize)
        {
            for (float z = 0; z < boardWidth; z += cellSize)
            {
                var point = CalculateGridPosition(new Vector3(x, 0.9f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }

    public void SaveBoardStateInList(int x, int z)
    {
        SavedBuilding sB = new SavedBuilding(x, z, buildings[x, z].buildingName);
        //Debug.Log("guardando objeto con id: " + sB.id);
        if (buildings[x, z].type == Building.Type.MATERIALGENERATORBUILDING)
            sB.currentMaterials = buildings[x, z].materialsPerSecond;
        else
            sB.currentMaterials = 0;

        SaveBoardStatus(x, z);
        SaveObject.Instance.buildingsInBoard.Add(sB);

    }

    private void SaveBoardStatus(int x, int z)
    {
        int cellsInZ = buildings[x, z].cellsInZ;
        int cellsInX = buildings[x, z].cellsInX;
        for (int r = 0; r < cellsInX; r++)
        {
            for (int c = 0; c < cellsInZ; c++)
            {
                boardOccupationStatus[r + x, c + z] = true;
            }
        }
    }

    public void InitializeBoard()
    {
        //Debug.Log("2. TEST: Inicializando el board");
        var savedBoardState = SaveObject.Instance.buildingsInBoard;

        if (savedBoardState.Count != 0)
        {
            foreach (SavedBuilding b in savedBoardState)
            {
                string path = buildingsPath + "/" + b.buildingName;
                GameObject prefabToInstantiate = Resources.Load<GameObject>(path);
                prefabToInstantiate.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                Vector3 position = new Vector3(b.row, 0f, b.col);
                //Debug.Log("currentMaterials de building añadidos: " + b.currentMaterials);
                AddBuilding(prefabToInstantiate, CalculatePosition(position), b.currentMaterials);
            }
            
        }
        else
        {
            // static buildings initial configuration. First time the game starts
            AddBuilding(staticBuildingA, CalculateGridPosition(new Vector3(UnityEngine.Random.Range(1, 29), 0, UnityEngine.Random.Range(1, 29))), -1);
            AddBuilding(staticBuildingA, CalculateGridPosition(new Vector3(UnityEngine.Random.Range(1, 29), 0, UnityEngine.Random.Range(1, 29))), -1);
            AddBuilding(library, CalculateGridPosition(new Vector3(UnityEngine.Random.Range(1, 29), 0, UnityEngine.Random.Range(1, 29))), -1);
        }

    }

}
 