using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Card.Resource myResource;
    public Image fill;

    private const int MAX_VALUE = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentFill(PlayerData.resources[myResource]);
    }

    public void SetCurrentFill(int current)
    {
        float fillQuantity = (float)current / (float)MAX_VALUE;
        fill.fillAmount = fillQuantity;
    }
}
