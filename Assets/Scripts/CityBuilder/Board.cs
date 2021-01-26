using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Building[,] buildings = new Building[100, 100];

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
        return new Vector3(Mathf.Round(position.x), .5f, Mathf.Round(position.z));
    }
}
