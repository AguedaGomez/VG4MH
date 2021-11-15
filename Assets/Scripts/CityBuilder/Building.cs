using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    //public Text indicatorText;
    //public int maxMaterials = 40;
    //public Button materialsButton;

    public string id; // id when building GO is created (string composed xz coordinates)
    public int cost; // materials needed to built it
    public string buildingName;
    public int nLocals; // number of new inhabitants that attracks
    public int activationRequired; // minimun of activation to built it
    public int row; // row (z) in the board where it was built
    public int col; // col (x) in the board where it was built
    public int cellsInRow; // all the cells that it occupies in z
    public int cellsInCol; // all the cells that it occupies in x
    public int powerRIncrease; // percentage of powerR that increases when built

    public int materialsPerSecond = 0;

    public enum Type
    {
        MATERIALGENERATORBUILDING,
        NONE
    }
    public Type type; // if the building generates materials
}
