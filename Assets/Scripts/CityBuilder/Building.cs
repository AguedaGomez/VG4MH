using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    
    private string id; // id when building GO is created (string composed xz coordinates)
    private int cost; // materials needed to built it
    private string buildingName;
    public GameObject specialCharacterPrefab; //Si está vacío significa que no tiene specialCharacter, si no, sí
    private int nLocals; // number of new inhabitants that attracks
    public int row; // row (z) in the board where it was built
    public int col; // col (x) in the board where it was built
    private int cellsInZ; // all the cells that it occupies in z, get from SO
    private int cellsInX; // all the cells that it occupies in x, get from SO
    private int powerRIncrease; // percentage of powerR that increases when built
    private int energyRequired;

    private Sprite silhouette;
    private Sprite image;

    public int materialsPerSecond = 0;


    private Construction.Type type; // if the building generates materials, get from SO


    public void InitializeBuildingPrefab(Construction data)
    {
        buildingName = data.buildingName;
        cost = data.cost;
        nLocals = data.nLocals;
        cellsInX = data.cellsInX;
        cellsInZ = data.cellsInZ;
        type = data.type;
        energyRequired = data.energyRequired;
        silhouette = data.silhouette;
        
    }

    public string GetId() { return id;  }
    public void SetId(string newId) { id = newId;  }
    public string GetName() { return buildingName; }
    public int GetCost() { return cost; }
    public int GetCellsX() { return cellsInX;  }
    public int GetCellsZ() { return cellsInZ;  }
    public int GetEnergyRequired() { return energyRequired; }
    public int GetNLocals() { return nLocals; }
    public Construction.Type GetBType() { return type; }
    public Sprite GetSilouette() { return silhouette;  }
    public Sprite GetImage() { return image;  }


}
