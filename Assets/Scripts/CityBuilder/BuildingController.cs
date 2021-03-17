using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public City city; //mirar cambiar city de aqui
    [SerializeField]
    private Board board;
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
                Building buildingScript = selectedBuilding.GetComponent<Building>();

                if (buildingScript.cost <= city.Materials)
                {
                    board.AddBuilding(selectedBuilding, gridPosition);

                    city.CalculatePopulation(buildingScript);
                    city.DecreaseMaterials(buildingScript.cost);
                }
                selectedBuilding = null;
            }
        }
    }
    public void EnableBuilder(string buildingName)
    {
       if(city.availableBuildigs.ContainsKey(buildingName))
        {
            selectedBuilding = city.availableBuildigs[buildingName];
        }
    }
}
