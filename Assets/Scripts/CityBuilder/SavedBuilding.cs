using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedBuilding
{
    public int row;
    public int col;
    public string idDic;
    public int currentMaterials;
    public string id;

    public SavedBuilding(int row, int col, string idDic) // TODO eliminar currentMaterials
    {
        this.row = row;
        this.col = col;
        this.idDic = idDic;
        this.currentMaterials = -1; // it will change if the building generates materiales
        id = row +"" + col + "";
    }
}
