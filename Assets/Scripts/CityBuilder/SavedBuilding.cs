using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedBuilding
{
    public int col;
    public int row;
    public string tag;
    public int currentMaterials;

    public SavedBuilding(int row, int col, string tag, int currentMaterials)
    {
        this.row = row;
        this.col = col;
        this.tag = tag;
        this.currentMaterials = currentMaterials;
    }
}
