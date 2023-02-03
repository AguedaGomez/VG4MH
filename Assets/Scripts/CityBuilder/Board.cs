using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    private string idSmallHouse = "smallHouse";
    private string idLibrary = "library";
    public City city; //Eliminar city de aquí?

    private const int UP_LIMIT = 29;
    private const int LEFT_LIMIT = 0;
    private const int DOWN_LIMIT = -1;
    private const int RIGHT_LIMIT = 30;

    private float cellSize = 4f;
    private int numCells;
    private float boardHeight, boardWidth;
    private Building[,] buildings;
    private BoardView boardView;

    private bool[,] boardOccupationStatus;
    private bool occupiedCell = true;
    
    private string buildingsPath = "Prefabs/CityBuilder/Buildings"; //coge los prefabs de GameManager
    [SerializeField] private NavMeshSurface navMesh;
    [SerializeField] private CitizensGenerator citizensGenerator;

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

    public void AddBuildingInEditMode(GameObject building, Vector3 positioninCells, int xCell, int zCell)
    {
        bool availability = CheckSpaceAtPosition(building, xCell, zCell);
        ChangeBuildingColor(availability);
        GameObject currentBuilding = boardView.SetUpBuildingEditMode(building, availability, positioninCells);
    }

    public bool CheckSpaceAtPosition(GameObject building, int xCell, int zCell)
    {
        //int x = CalculateRowColumn(position.x);
        //int z = CalculateRowColumn(position.z);
        Building buildingScript = building.GetComponent<Building>();
        //return CheckAvailableSpace((xCell, zCell, buildingScript) && CheckForBuildingAtPosition(position);
        return CheckAvailableSpace(xCell, zCell, buildingScript);
    }

    public void ChangeBuildingColor(bool availability)
    {
        boardView.ColorDependingAvailability(availability);
    }

    public void AddBuilding(GameObject building, Vector3 position, int currentMaterials, bool newBuilding, int x, int z)
    {
        if (CheckSpaceAtPosition(building, x, z))
        {
            Debug.Log("Current position: " + position);
            Transform buildingTransform = building.GetComponent<Transform>();
            Quaternion buildingRotation = buildingTransform.rotation;
            GameObject createdBuilding = Instantiate(building, position, buildingRotation);
            Building buildingScript = createdBuilding.GetComponent<Building>();
            buildingScript.InitializeBuildingPrefab(GameManager.Instance.buildingInConstruction);
            createdBuilding.transform.name = buildingScript.GetName();

            //int x = CalculateRowColumn(position.x);
            //int z = CalculateRowColumn(position.z);

            Debug.Log("Introduciendo building en x: " + x + "z: " + z);

            buildingScript.SetId(x + "" + z + "");
            GameManager.Instance.buildingInConstruction = null;

            if (buildingScript.GetBType() == Construction.Type.MATERIALGENERATORBUILDING)
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
                    mGBScript.SetCurrentMaterials(currentMaterials, city.inactiveTime.Seconds);
                }
                    
            } else
            {
                buildings[x, z] = buildingScript;
                
                if (currentMaterials < 0)
                    SaveBoardStateInList(x, z);
            }

            navMesh.BuildNavMesh();
            GameManager.Instance.buildingInConstruction = null;

            //Si es false significa que está cargando desde el SaveObject por lo que no requiere generar ciudadanos
            if (newBuilding)
            {
                citizensGenerator.AddCitizens(buildingScript);

                if (buildingScript.specialCharacterPrefab != null)
                {
                    Debug.Log("special character es " + buildingScript.specialCharacterPrefab.name);
                    citizensGenerator.AddSpecialCitizen(buildingScript.specialCharacterPrefab);
                }
            }
        }
        else
        {
            //mostrar mensaje de que no se puede construir ahi
        }

    }

    public bool CheckForBuildingAtPosition(Vector3 position)
    {
        //return buildings[CalculateRowColumn(position.x), CalculateRowColumn(position.z)] == null;
        return buildings[(int)position.x, (int)(position.z)] == null;
    }

    private bool CheckAvailableSpace(int x, int z, Building currentBuilding)
    {
        //for (int r = 0; r < currentBuilding.GetCellsX(); r++)
        //{
        //    for (int c = 0; c < currentBuilding.GetCellsZ(); c++)
        //    {
        //        if (boardOccupationStatus[x + r, z - c] == occupiedCell) // there isn't available space
        //            return false;
        //    }
        //}
        //return true;
        int cellsInZ = currentBuilding.GetCellsX();
        int cellsInX = currentBuilding.GetCellsZ();

        int cellsInZMitad = cellsInZ / 2;
        int cellsInXMitad = cellsInX / 2;
        int final;

        if (cellsInZ % 2 == 0) final = cellsInZMitad - 1;
        else final = cellsInZMitad;

        for (int r = -cellsInXMitad; r <= final; r++)
        {
            for (int c = -cellsInZMitad; c <= final; c++)
            {
                if (boardOccupationStatus[x + c, z + r] == occupiedCell) return false;
            }
        }
        return true;
    }

    public bool CheckBoardLimits(Vector3 gridPosition, Building buildingScript)
    {
        int x = CalculateRowColumn(gridPosition.x);
        int z = CalculateRowColumn(gridPosition.z);
        if (x < LEFT_LIMIT ||  z > UP_LIMIT || x + buildingScript.GetCellsX() > RIGHT_LIMIT || z - buildingScript.GetCellsZ() < DOWN_LIMIT) //out of the limits
            return true;
        return false;
    }

    public Vector3 CalculateGridPosition(Vector3 position, int bSize, out int xCell, out int zCell)
    {
        //Debug.Log("Posición a convertir: " + position);
        xCell = CalculateRowColumn(position.x);
        float yCell = .5f;
        zCell = CalculateRowColumn(position.z);

        if (bSize % 2 == 0)
            return new Vector3(xCell * cellSize-2, yCell, zCell * cellSize-2);

        else 
            return new Vector3(xCell*cellSize, yCell, zCell*cellSize); 
    }
    private Vector3 CalculatePosition(Vector3 gridPosition, int bSize)
    {
        if (bSize % 2 == 0)
            return new Vector3(gridPosition.x * cellSize - 2, .5f, gridPosition.z * cellSize - 2);

        else
            return new Vector3(gridPosition.x * cellSize, .5f, gridPosition.z * cellSize);
    }

    public int CalculateRowColumn(float cordPosition)
    {
        return Mathf.RoundToInt(cordPosition / cellSize); // number of cell
    }


    public void SaveBoardStateInList(int x, int z)
    {
        SavedBuilding sB = new SavedBuilding(x, z, buildings[x, z].GetIdData());

        if (buildings[x, z].GetBType() == Construction.Type.MATERIALGENERATORBUILDING)
            sB.currentMaterials = buildings[x, z].materialsPerSecond;
        else
            sB.currentMaterials = 0;

        SaveBoardStatus(x, z);
        //Debug.Log("Guardando el edificio " + sB.idData + " en las coordenadas " + sB.row + sB.col);
        SaveObject.Instance.buildingsInBoard.Add(sB);

    }

    private void SaveBoardStatus(int x, int z) //celdas in x y z son iguales
    {
        int cellsInZ = buildings[x, z].GetCellsZ();
        int cellsInX = buildings[x, z].GetCellsX();

        int cellsInZMitad = cellsInZ/2;
        int cellsInXMitad = cellsInX/2;
        int final;

        if (cellsInZ % 2 == 0) final = cellsInZMitad - 1;
        else final = cellsInZMitad;

        for (int r = -cellsInXMitad; r <= final; r++)
        {
            for (int c = -cellsInZMitad; c <= final; c++)
            {
                boardOccupationStatus[x + c, z + r] = true;
            }
        }
    }

    public void InitializeBoard()
    {
        //Debug.Log("2. TEST: Inicializando el board");
        var savedBoardState = SaveObject.Instance.buildingsInBoard;
        //Debug.Log("Edificación: " + savedBoardState[0].buildingName + " , " + savedBoardState[0].currentMaterials + " , " + savedBoardState[0].id + " ( " + savedBoardState[0].col + " . " + savedBoardState[0].row);

        if (savedBoardState.Count != 0)
        {
            Camera mainCamera = Camera.main;

            foreach (SavedBuilding b in savedBoardState)
            {
                //string path = buildingsPath + "/" + b.idData;
                //Debug.Log("Se ha mirado la dirección del prefab: " + path);
                Debug.Log("prefab id " + b.idData);
                GameObject prefabToInstantiate = GameManager.Instance.buildingsInGame[b.idData].prefab;
                //Debug.Log("Se ha encontrado " + prefabToInstantiate.name);
                GameManager.Instance.buildingInConstruction = GameManager.Instance.buildingsInGame[b.idData];
                if (GameManager.Instance.buildingsInGame[b.idData].type == Construction.Type.MATERIALGENERATORBUILDING)
                    prefabToInstantiate.GetComponentInChildren<Canvas>().worldCamera = mainCamera;
                Vector3 position = new Vector3(b.row, 0f, b.col);
                //Debug.Log("currentMaterials de building añadidos: " + b.currentMaterials);
                AddBuilding(prefabToInstantiate, CalculatePosition(position, GameManager.Instance.buildingsInGame[b.idData].cellsInX), b.currentMaterials, false, b.row, b.col);
            }
            
        }
        else
        {
            int xCell, zCell;
            Vector3 currentPosition;
            // static buildings initial configuration. First time the game starts
            GameManager.Instance.buildingInConstruction = GameManager.Instance.buildingsInGame[idSmallHouse];
            currentPosition = CalculateGridPosition(new Vector3(16, 0, 26), 1, out xCell, out zCell);

            AddBuilding(GameManager.Instance.buildingInConstruction.prefab, currentPosition, -1, true, xCell, zCell);
            GameManager.Instance.buildingInConstruction = GameManager.Instance.buildingsInGame[idSmallHouse];
            currentPosition = CalculateGridPosition(new Vector3(15, 0, 16), 1, out xCell, out zCell);

            AddBuilding(GameManager.Instance.buildingInConstruction.prefab, currentPosition, -1, true, xCell, zCell);
            GameManager.Instance.buildingInConstruction = GameManager.Instance.buildingsInGame[idLibrary];
            currentPosition = CalculateGridPosition(new Vector3(23, 0, 26), 2, out xCell, out zCell);
            AddBuilding(GameManager.Instance.buildingInConstruction.prefab, currentPosition, -1, true, xCell, zCell);
            GameManager.Instance.buildingInConstruction = null;
        }

    }

    public static Vector3 GetRandomPoint() 
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int t = Random.Range(0, navMeshData.indices.Length - 3);

        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }

}
 