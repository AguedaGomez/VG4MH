using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public Text indicatorText;
    public const int MAX_MATERIALS = 40;
    
    public delegate void MaterialLimitEventHandler(int materials);
    public event MaterialLimitEventHandler FullOfMaterials;

    public int id;
    public int cost;
    public string buildingName;
    public int nLocals;
    public int activationRequired;
    public int row;
    public int col;

    private int materials = 0;
    private int materialsPerSecond = 0;
    private const int MIN_MATERIALS = 40;
    private const float SECONDS = 30;
    private const string INDICATOR_BEGINNING = "materiales: ";

    void Start()
    {
        StartCoroutine(CalculateMaterialsPerSecond());
    }
    IEnumerator CalculateMaterialsPerSecond()
    {
        while (true)
        {
            if (materials <= MAX_MATERIALS)
            {
                materialsPerSecond += MIN_MATERIALS; //* CalculatePowerRFactor();
                materials = materialsPerSecond;
            }
            else
                StopCoroutine(CalculateMaterialsPerSecond());
            indicatorText.text = INDICATOR_BEGINNING + materials + "/" + MAX_MATERIALS;
            yield return new WaitForSeconds(SECONDS);
        }

    }

    public void PickMaterials()
    {
        FullOfMaterials(materials);
        materialsPerSecond = 0;
        materials = 0;
        StartCoroutine(CalculateMaterialsPerSecond());
    }
}
