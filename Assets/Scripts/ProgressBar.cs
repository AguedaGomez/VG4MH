using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Card.Resource myResource;
    public Image fill;

    private const int MAX_VALUE = 100;
    private float currentFillQuantity;

    // Start is called before the first frame update
    void Start()
    {
        currentFillQuantity = (float)PlayerData.resources[myResource] / (float)MAX_VALUE;
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentFill(PlayerData.resources[myResource]);
    }

    public void SetCurrentFill(int current)
    {
        float newFillQuantity = (float)current / (float)MAX_VALUE;
        if (currentFillQuantity < newFillQuantity)
        {
            StartCoroutine(ChangeColor(Palette.GREEN));
        }
        else if (currentFillQuantity > newFillQuantity)
        {
            StartCoroutine(ChangeColor(Palette.RED));
        }
        fill.fillAmount = newFillQuantity;
        currentFillQuantity = newFillQuantity;

    }

    IEnumerator ChangeColor(Color32 color)
    {
        fill.color = color;
        yield return new WaitForSeconds(.5f);
        fill.color = Palette.BLUE;
    } 
}
