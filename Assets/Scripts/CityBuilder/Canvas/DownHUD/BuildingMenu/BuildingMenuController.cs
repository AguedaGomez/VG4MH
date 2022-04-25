using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuController : MonoBehaviour
{
    public GameObject bGridElement; // Prefab que se va a instanciar en la lista
    public GameObject GridWithBuildings; // Referencia al padre donde bajo el que se va a crear
    
    public InteractionController interactionController; // Referencia al script asignado a Ground para poder añadir el onclick construir (Enable builder)
    // Datos disponibles sobre los edificios, prefab también dentro del scriptable object? coger desde game manager
    // Método que recorra lista y posicione todos los elementos
    void Start()
    {
        CreateBuildingGrid();
    }

    public void CreateBuildingGrid()
    {
        foreach (var construction in GameManager.Instance.buildingsInGame)
        {
            GameObject gridElement = Instantiate(bGridElement, GridWithBuildings.transform);
            Transform gridElementTransform = gridElement.transform;
            gridElementTransform.Find("Name").GetComponent<Text>().text = construction.buildingName;
            gridElementTransform.Find("Image").GetComponent<Image>().sprite = construction.image;
            //gridElementTransform.Find("Image").GetComponent<Image>().SetNativeSize();
            gridElementTransform.Find("Mat Number").GetComponent<Text>().text = "" + construction.maximunMaterials;
            gridElementTransform.Find("Build").transform.Find("Text").GetComponent<Text>().text = "" + construction.cost;
            gridElementTransform.Find("GridNumber").GetComponent<Text>().text = "" + construction.cellsInX + "x" + construction.cellsInZ;
            gridElementTransform.Find("Build").GetComponent<Button>().onClick.AddListener(()=>interactionController.EnableBuilder(construction.id));
            //Rellenar los datos en el prefab con los scriptable objects?
        }
    }
}
