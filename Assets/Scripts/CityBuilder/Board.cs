using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private int cellSize = 2;
    private float boardHeight, boardWidth;
    private int numCells;
    private Building[,] buildings;

    void Start()
    {
        boardWidth = transform.localScale.x;
        boardHeight = transform.localScale.z;

        numCells = (int)boardWidth / cellSize; //celdas cuadradas
        buildings = new Building[numCells, numCells];

       // print("board height = " + boardHeight + " board width = " + boardWidth + " num celdas = " + numCells);
    }

    public void AddBuilding(GameObject building, Vector3 position)
    {
        print("building name: " + building.name);
        if (CheckForBuildingAtPosition(position))
        {
            print("instantiate");
            GameObject createdBuilding = Instantiate(building, position, Quaternion.identity);
            buildings[(int)position.x, (int)position.z] = createdBuilding.GetComponent<Building>();

        }
        
    }

    public bool CheckForBuildingAtPosition(Vector3 position)
    {
        return buildings[(int)position.x, (int)position.z] == null;
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        int xCell = Mathf.RoundToInt(position.x / cellSize);
        float yCell = .5f;
        int zCell = Mathf.RoundToInt(position.z / cellSize);

        return new Vector3(xCell*cellSize, yCell, zCell*cellSize);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.white;
    //    for (float x = 0; x < 30; x += CELLSIZE)
    //    {
    //        for (float z = 0; z < 30; z += CELLSIZE)
    //        {
    //            var point = CalculateGridPosition(new Vector3(x, 0.9f, z));
    //            Gizmos.DrawSphere(point, 0.1f);
    //        }
    //    }
    //}
}
