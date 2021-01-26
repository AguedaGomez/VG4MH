using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    private Board board;
    public GameObject[] buildings; //Prefabs available buildings
    private GameObject selectedBuilding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   void Update()
    {
        if( Input.GetMouseButtonDown(0) && selectedBuilding != null)
        {
            InteractWithBoard();
        }
    }

    void InteractWithBoard()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // change for mobile
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            print("ray");
            Vector3 gridPosition = board.CalculateGridPosition(hit.point);
            if (board.CheckForBuildingAtPosition(gridPosition))
            {
                board.AddBuilding(selectedBuilding, gridPosition);
                selectedBuilding = null;
            }
        }
    }
    public void EnableBuilder(int buildingIndex)
    {
        selectedBuilding = buildings[buildingIndex];
        
    }
}
