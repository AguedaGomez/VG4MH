using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // TODO: configuracion inicial
    public GameObject staticBuildingA;
    public GameObject staticBuildingB;
    public City city; //Eliminar city de aquí?

    private float cellSize = 4f;
    private float boardHeight, boardWidth;
    private int numCells;
    private Building[,] buildings;
    
    private string buildingsPath = "Prefabs/CityBuilder/Buildings";

    void Start()
    {


        // static buildings initial configuration
        //AddBuilding(staticBuildingA, CalculateGridPosition(new Vector3(8, 0, 10)));
        //AddBuilding(staticBuildingB, CalculateGridPosition(new Vector3(16, 0, 11)));
        //AddBuilding(staticBuildingA, CalculateGridPosition(new Vector3(12, 0, 15)));

    }

    public void AddBuilding(GameObject building, Vector3 position)
    {

        if (CheckForBuildingAtPosition(position))
        {
            GameObject createdBuilding = Instantiate(building, position, Quaternion.identity);
            Building buildingScript = createdBuilding.GetComponent<Building>();
            createdBuilding.transform.name = buildingScript.buildingName;
            buildings[CalculateRowColumn(position.x), CalculateRowColumn(position.z)] = buildingScript;

        } 
    }

    public bool CheckForBuildingAtPosition(Vector3 position)
    {
        return buildings[CalculateRowColumn(position.x), CalculateRowColumn(position.z)] == null;
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

    public void SaveBoardStateInList(out List<SavedBuilding> savedBuildings)
    {
        savedBuildings = new List<SavedBuilding>();

        for (int x = 0; x < buildings.GetLength(0); x++)
        {
            for (int z = 0; z < buildings.GetLength(1); z++)
            {
                if (buildings[x, z] != null)
                {
                    SavedBuilding sB = new SavedBuilding(x, z, buildings[x, z].buildingName, buildings[x, z].materials);
                    savedBuildings.Add(sB);
                }
            }
        }

    }

    public void InitializeBoard(List<SavedBuilding> savedBoardState)
    {
        boardWidth = transform.localScale.x;
        boardHeight = transform.localScale.z;

        numCells = (int)(boardWidth / cellSize); //celdas cuadradas
        buildings = new Building[numCells, numCells];

        if (savedBoardState.Count != 0)
        {
            foreach (SavedBuilding b in savedBoardState)
            {
                string path = buildingsPath + "/" + b.buildingName;
                GameObject prefabToInstantiate = Resources.Load<GameObject>(path);
                Vector3 position = new Vector3(b.row, 0f, b.col);
                //GameObject savedBuilding = Instantiate(prefabToInstantiate, position, Quaternion.identity);
                //Building buildingScript = savedBuilding.GetComponent<Building>();
                AddBuilding(prefabToInstantiate, CalculatePosition(position));
                //buildingScript.materials = b.currentMaterials;
            }
        }
        else
        {

            // static buildings initial configuration
            AddBuilding(staticBuildingA, CalculateGridPosition(new Vector3(8, 0, 10)));
            AddBuilding(staticBuildingB, CalculateGridPosition(new Vector3(16, 0, 11)));
            AddBuilding(staticBuildingA, CalculateGridPosition(new Vector3(12, 0, 15)));
        }

    }
}
 