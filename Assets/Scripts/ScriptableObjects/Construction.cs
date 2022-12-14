using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Construction")]
public class Construction : ScriptableObject
{
    public Sprite image;
    public Sprite silhouette;
    public GameObject prefab;
    public string id; // id when building GO is created (string composed xz coordinates)
    public string idData;
    public int cost; // materials needed to built it
    public string buildingName;
    public int nLocals; // number of new inhabitants that attracks
    public int energyRequired; // minimun of activation to built it
    public int cellsInZ; // all the cells that it occupies in z
    public int cellsInX; // all the cells that it occupies in x
    public int powerRIncrease; // percentage of powerR that increases when it is built

    public int materialsPerSecond = 0;
    public int maximunMaterials; // maximun materials that construction can collect

    public enum Type
    {
        MATERIALGENERATORBUILDING,
        NONE
    }
    public Type type; // if the building generates materials
}
