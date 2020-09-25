using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDisplay : MonoBehaviour
{

    public delegate void UpdateResourceEventHandler(Card.Resource currentResource);
    public event UpdateResourceEventHandler UpdateResource;

    // slide de cada uno de los iconos
    public GameObject motivationUI;
    public GameObject flexibilityUI;
    public GameObject activationUI;
    public GameObject positiveUI;

    private ProgressBar progressBarMotivation;
    private ProgressBar progressBarFlexibility;
    private ProgressBar progressBarActivation;
    private ProgressBar progressBarPositive;

    // texto


    void Start()
    {
        progressBarMotivation = motivationUI.GetComponent<ProgressBar>();
        progressBarFlexibility = flexibilityUI.GetComponent<ProgressBar>();
        progressBarActivation = activationUI.GetComponent<ProgressBar>();
        progressBarPositive = positiveUI.GetComponent<ProgressBar>();
    }


    public void UpdateResourceVisualization(Card.Resource currentResource)
    {
        UpdateResource(currentResource);
    }
    //public void UpdateResource(Card.Resource currentResource)
    //{
    //    switch (currentResource)
    //    {
    //        case Card.Resource.MOTIVATION:
    //            progressBarMotivation.SetCurrentFill(PlayerData.motivationQuantity);
    //            break;
    //        case Card.Resource.FLEXIBILITY:
    //            progressBarFlexibility.SetCurrentFill(PlayerData.flexibilityQuantity);
    //            break;
    //        case Card.Resource.ACTIVATION:
    //            progressBarActivation.SetCurrentFill(PlayerData.activationQuantity);
    //            break;
    //        case Card.Resource.POSITIVE:
    //            progressBarPositive.SetCurrentFill(PlayerData.positiveQuantity);
    //            break;
    //        case Card.Resource.NONE:
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
