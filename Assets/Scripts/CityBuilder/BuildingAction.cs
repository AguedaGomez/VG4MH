using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingAction : MonoBehaviour
{
    public void ChangeScene (string nameScene)
    {
        GameManager.Instance.SaveGame();

        SceneManager.LoadScene(nameScene);
    }

    public void BacktoTown()
    {
        GameManager.Instance.LoadGame();
    }

    public void PickUpMaterials()
    {
        this.GetComponent<MaterialGeneratorBuilding>().PickMaterials();
    }
}
