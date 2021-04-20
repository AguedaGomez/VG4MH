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
    [SerializeField]
    private Building[,] buildings;

    void Start()
    {
        boardWidth = transform.localScale.x;
        boardHeight = transform.localScale.z;

        numCells = (int)(boardWidth / cellSize); //celdas cuadradas
        buildings = new Building[numCells, numCells];

        // static buildings initial configuration
        AddBuilding(staticBuildingA, new Vector3(9, 0, 13));
        AddBuilding(staticBuildingB, new Vector3(16, 0, 20));
        AddBuilding(staticBuildingA, new Vector3(17, 0, 14));

    }

    public void AddBuilding(GameObject building, Vector3 position)
    {

        if (CheckForBuildingAtPosition(position))
        {
            GameObject createdBuilding = Instantiate(building, position, Quaternion.identity);
            Building buildingScript = createdBuilding.GetComponent<Building>();
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
                    SavedBuilding sB = new SavedBuilding(x, z, buildings[x, z].tag, buildings[x, z].materials);
                    savedBuildings.Add(sB);
                }
            }
        }

    }
}
 