using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public City city; //Eliminar city de aquí?

    private const int UP_LIMIT = 29;
    private const int LEFT_LIMIT = 0;
    private const int DOWN_LIMIT = -1;
    private const int RIGHT_LIMIT = 30;

    private const string SMALL_HOUSE = "1"; //smallHouse
    private const string LIBRARY = "17"; //library

    private float cellSize = 4f;
    private int numCells;
    private float boardHeight, boardWidth;
    private Building[,] buildings; //OJO SE NECESITAN ACCESIBLES LOS SCRIPTS DE CADA UNO DE LOS EDIFICIOS CONSTRUIDOS
    private BoardView boardView;

    private bool[,] boardOccupationStatus;
    private bool occupiedCell = true;
    
   // private string buildingsPath = "Prefabs/CityBuilder/Buildings"; //es necesario?
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

    public void AddBuildingInEditMode(Vector3 position)
    {
        bool availability = CheckSpaceAtPosition(position);
        ChangeBuildingColor(availability);
        GameObject currentBuilding = boardView.SetUpBuildingEditMode(building, availability, position); //PARA QUÉ CURRENTBUILDING
    }

    public bool CheckSpaceAtPosition(Vector3 position)
    {
        int x = CalculateRowColumn(position.x);
        int z = CalculateRowColumn(position.z);

        return CheckAvailableSpace(x, z) && CheckForBuildingAtPosition(position);
       
    }

    public void ChangeBuildingColor(bool availability)
    {
        boardView.ColorDependingAvailability(availability);
    }
    public void AddBuilding(Vector3 position, int currentMaterials) // materiales con los que se guardó?
    {
        if (CheckSpaceAtPosition(position))
        {

            Transform buildingTransform = GameManager.Instance.buildingInConstruction.prefab.GetComponent<Transform>();
            Quaternion buildingRotation = buildingTransform.rotation;
            GameObject createdBuilding = Instantiate(GameManager.Instance.buildingInConstruction.prefab, position, buildingRotation);
            Building buildingScript = createdBuilding.GetComponent<Building>();
            createdBuilding.transform.name = GameManager.Instance.buildingInConstruction.buildingName;

            int x = CalculateRowColumn(position.x);
            int z = CalculateRowColumn(position.z);

            buildingScript.id = x + "" + z + "";

            if (GameManager.Instance.buildingInConstruction.type == Construction.Type.MATERIALGENERATORBUILDING)
            {
                MaterialGeneratorBuilding mGBScript = createdBuilding.GetComponent<MaterialGeneratorBuilding>();
                buildings[x, z] = mGBScript;

                if (currentMaterials < 0) //¿qué es currentMaterials?
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

            navMesh.BuildNavMesh();
            citizensGenerator.AddCitizens(buildingScript);
            
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

    private bool CheckAvailableSpace(int x, int z) //BORRAR CURRENTBUILDING DE PARÁMETROS
    {
        for (int r = 0; r < GameManager.Instance.buildingInConstruction.cellsInZ; r++)
        {
            for (int c = 0; c < GameManager.Instance.buildingInConstruction.cellsInZ; c++)
            {
                if (boardOccupationStatus[x + r, z - c] == occupiedCell) // there isn't available space
                    return false;
            }
        }
        return true;
    }

    public bool CheckBoardLimits(Vector3 gridPosition)
    {
        int x = CalculateRowColumn(gridPosition.x);
        int z = CalculateRowColumn(gridPosition.z);
        int cellsInX = GameManager.Instance.buildingInConstruction.cellsInX;
        int cellsInZ = GameManager.Instance.buildingInConstruction.cellsInZ;
        //if (x < LEFT_LIMIT ||  z > UP_LIMIT || x + buildingScript.cellsInX > RIGHT_LIMIT || z - buildingScript.cellsInZ < DOWN_LIMIT) //out of the limits
        //    return true;
        //return false;
        if (x < LEFT_LIMIT || z > UP_LIMIT || x + cellsInX > RIGHT_LIMIT || z - cellsInZ < DOWN_LIMIT) //out of the limits
            return true;
        return false;
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

    public void SaveBoardStateInList(int x, int z)
    {
        SavedBuilding sB = new SavedBuilding(x, z, buildings[x, z].builtId); //en buildings se garda solo el id
        Construction currentConstruction = GameManager.Instance.buildingsInGame[buildings[x, z].id];
        var type = currentConstruction.type;
        if (type == Construction.Type.MATERIALGENERATORBUILDING)
            sB.currentMaterials = currentConstruction.materialsPerSecond; // OJO materialsPerSecond del script
        else
            sB.currentMaterials = 0;

        SaveBoardStatus(x, z);
        SaveObject.Instance.buildingsInBoard.Add(sB);

    }

    private void SaveBoardStatus(int x, int z)
    {
        int cellsInZ = GameManager.Instance.buildingInConstruction.cellsInZ;
        int cellsInX = GameManager.Instance.buildingInConstruction.cellsInZ;
        for (int r = 0; r < cellsInX; r++)
        {
            for (int c = 0; c < cellsInZ; c++)
            {
                boardOccupationStatus[x + r, z - c] = true;
            }
        }
    }

    public void InitializeBoard()
    {
       // cuando hay partida guardada
        var savedBoardState = SaveObject.Instance.buildingsInBoard;

        if (savedBoardState.Count != 0)
        {
            foreach (SavedBuilding b in savedBoardState)
            {
                GameManager.Instance.buildingInConstruction = GameManager.Instance.buildingsInGame[b.idDic];
                GameObject prefabToInstantiate = GameManager.Instance.buildingInConstruction.prefab;
                prefabToInstantiate.GetComponentInChildren<Canvas>().worldCamera = Camera.main;
                Vector3 position = new Vector3(b.row, 0f, b.col);
                
                AddBuilding(CalculatePosition(position), b.currentMaterials);
            }
            
        }
        else
        {
            // static buildings initial configuration. First time the game starts
            //AddBuilding(staticBuildingA, CalculateGridPosition(new Vector3(UnityEngine.Random.Range(1, 29), 0, UnityEngine.Random.Range(1, 29))), -1);
            //AddBuilding(staticBuildingA, CalculateGridPosition(new Vector3(UnityEngine.Random.Range(1, 29), 0, UnityEngine.Random.Range(1, 29))), -1);
            //AddBuilding(library, CalculateGridPosition(new Vector3(UnityEngine.Random.Range(1, 29), 0, UnityEngine.Random.Range(1, 29))), -1);
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

    private void CreateStaticBuildings() {
        GameManager.Instance.buildingInConstruction = GameManager.Instance.buildingsInGame[SMALL_HOUSE];
        AddBuilding(GenerateRandomBoardPosition(), -1);
        AddBuilding(GenerateRandomBoardPosition(), -1);
        GameManager.Instance.buildingInConstruction = GameManager.Instance.buildingsInGame[LIBRARY];
    }

    private Vector3 GenerateRandomBoardPosition()
    {
        return new Vector3(Random.Range(1, 29), 0, Random.Range(1, 29));
    }

}
 