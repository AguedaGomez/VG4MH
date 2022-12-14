using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuController : MonoBehaviour
{
    public GameObject bGridElement; // Prefab que se va a instanciar en la lista
    public GameObject GridWithBuildings; // Referencia al padre donde bajo el que se va a crear

    public CanvasController canvasController; //Controlador de vista y referencia a Interaction Controller
    // Datos disponibles sobre los edificios, prefab también dentro del scriptable object? coger desde game manager
    // Método que recorra lista y posicione todos los elementos
    private Dictionary<string, BGridElementController> gridElementsDic = new Dictionary<string, BGridElementController>();
    void Start()
    {
        //CreateBuildingGrid();
    }

    public void CreateBuildingGrid()
    {
        foreach (var bData in GameManager.Instance.buildingsInGameList)
        {
            if (bData.buildingName != "Biblioteca")
            {
                GameObject gridElement = Instantiate(bGridElement, GridWithBuildings.transform);

                //En cada script controlador del elemento de la grid, se incorpora el id de la construcción correspondiente
                //Por defecto, no se puede contruir nada hasta, todos deben estar bloqueados
                BGridElementController bGridElementController = gridElement.GetComponent<BGridElementController>();
                bGridElementController.myId = bData.idData;
                bGridElementController.Lock();

                Transform gridElementTransform = gridElement.transform;
                gridElementTransform.Find("Build").GetComponent<Button>().onClick.AddListener(() => canvasController.SaveBuildingToConstruct(bData));
                //Para acceder al elemento de la lista que corresponde con una construcción concreta
                gridElementsDic.Add(bData.id, bGridElementController);
            }
        }

    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
        //llamar a hidemenu tb del padre
    }

    public void UnlockGridElement(string id)
    {
        gridElementsDic[id].UnLock();
    }

    public void LockAllGridElements()
    {
        for (int i = 0; i < gridElementsDic.Count; i++)
        {
            gridElementsDic[i + ""].Lock();
        }
    }
}
