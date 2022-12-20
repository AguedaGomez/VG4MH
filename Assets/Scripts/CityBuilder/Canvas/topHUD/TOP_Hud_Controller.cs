using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TOP_Hud_Controller : MonoBehaviour
{
    [SerializeField] Animator posShining;
    [SerializeField] Animator actShining;
    [SerializeField] Animator motShining;
    [SerializeField] Animator flexShining;
    [SerializeField] Animator materialsShining;

    [SerializeField] GameObject power_R_Slider;
    [SerializeField] GameObject activation_Slider;
    [SerializeField] Text materialsText;
    bool isMaterialShining = false;

    Card.Resource currentResource;

    private float currentPowerR, currentEnergy;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSliders();
    }

    private void InitializeSliders()
    {

        UpdateValueSlider(power_R_Slider, SaveObject.Instance.powerR);
        currentPowerR = SaveObject.Instance.powerR;

        UpdateValueSlider(activation_Slider, SaveObject.Instance.energy);
        currentEnergy = SaveObject.Instance.energy;

        
        updateMaterials_Text(SaveObject.Instance.materials);

    }

    private void UpdateValueSlider (GameObject slider, float value)
    {
        slider.GetComponent<Image>().fillAmount = value / 100;
    }

    public void UpdateEnergySliderwithAnimation(float newValue)
    {
        StartActValueUpdate(true, newValue);
    }

    public void UpdatePowerRSliderwithAnimation(float newValue)
    {
        StartPowerRValueUpdate(true, newValue);
    }

    public void Start_VisualResourceStatChange(Card.Resource resource, int valueModifier)
    {
        if(valueModifier != 0)
        {
            switch (resource)
            {
                case Card.Resource.ACTIVATION:
                    StartPowerRValueUpdate(valueModifier > 0, valueModifier);
                    Start_IconShining(Card.Resource.ACTIVATION);
                    break;
                case Card.Resource.FLEXIBILITY:
                    StartPowerRValueUpdate(valueModifier > 0, valueModifier);
                    Start_IconShining(Card.Resource.FLEXIBILITY);
                    break;
                case Card.Resource.MOTIVATION:
                    StartPowerRValueUpdate(valueModifier > 0, valueModifier);
                    Start_IconShining(Card.Resource.MOTIVATION);
                    break;
                case Card.Resource.PHYSICALACT:
                    StartPowerRValueUpdate(valueModifier > 0, valueModifier);
                    StartActValueUpdate(valueModifier > 0, valueModifier);
                    Start_IconShining(Card.Resource.ACTIVATION);
                    break;
                case Card.Resource.POSITIVE:
                    StartPowerRValueUpdate(valueModifier > 0, valueModifier);
                    Start_IconShining(Card.Resource.POSITIVE);
                    break;
                case Card.Resource.NONE:
                    break;
            }
        }
    }

    public void StartPowerRValueUpdate(bool increasing, float newValue)
    {
        if (increasing)
        {
            var Shape = power_R_Slider.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            Shape.rotation = new Vector3(0, 90, 0);
        }
        else
        {
            var Shape = power_R_Slider.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            Shape.rotation = new Vector3(0, -90, 0);
        }

        StartCoroutine(update_PowerRSliderValue(newValue));
    }

    IEnumerator update_PowerRSliderValue(float newPowerR)
    {
        float percentage_powerR = newPowerR / 100;
        float prev_Percentage = power_R_Slider.GetComponent<Image>().fillAmount; //COGER VALOR DEL PLAYER?
        percentage_powerR += prev_Percentage;

        RectTransform edgeRect = power_R_Slider.transform.GetChild(0).GetComponent<RectTransform>();
        ParticleSystem barParticles = power_R_Slider.transform.GetChild(0).GetComponent<ParticleSystem>();
        barParticles.Play();

        float timeToGo = 3f;
        float elapsedTime = 0;

        while (elapsedTime <= timeToGo)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(prev_Percentage, percentage_powerR, (elapsedTime / timeToGo));
            power_R_Slider.GetComponent<Image>().fillAmount = newValue;
            SaveObject.Instance.powerR = newValue * 100;
            edgeRect.anchorMin = new Vector2(newValue, newValue);
            edgeRect.anchoredPosition = new Vector2(0, 0);

            yield return null;
        }

        barParticles.Stop();
        power_R_Slider.GetComponent<Image>().fillAmount = percentage_powerR;
        SaveObject.Instance.powerR = percentage_powerR * 100;

        //Debug.Log("porcentaje de powerR: " + percentage_powerR);
        Stop_IconShining(currentResource);
        yield return null;
    }

    public void StartActValueUpdate(bool increasing, float newValue)
    {
        if (increasing)
        {
            var Shape = activation_Slider.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            Shape.rotation = new Vector3(0, 90, 0);
        }
        else
        {
            var Shape = activation_Slider.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            Shape.rotation = new Vector3(0, -90, 0);
        }

        //Ejecutar la animación correspondiente
        StartCoroutine(update_ActivationSliderValue(newValue));
    }

    IEnumerator update_ActivationSliderValue(float newActivationValue)
    {
        float percentage_Activation = newActivationValue / 100;
        float prev_Percentage = activation_Slider.GetComponent<Image>().fillAmount;
        RectTransform edgeRect = activation_Slider.transform.GetChild(0).GetComponent<RectTransform>();
        ParticleSystem barParticles = activation_Slider.transform.GetChild(0).GetComponent<ParticleSystem>();
        barParticles.Play();

        float timeToGo = 3f;
        float elapsedTime = 0;

        while (elapsedTime <= timeToGo)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(prev_Percentage, percentage_Activation, (elapsedTime / timeToGo));
            activation_Slider.GetComponent<Image>().fillAmount = newValue;
            edgeRect.anchorMin = new Vector2(newValue, newValue);
            edgeRect.anchoredPosition = new Vector2(0, 0);


            yield return null;
        }

        barParticles.Stop();
        //iconAnimation.SetBool("finishedSliderUpdating", true);
        activation_Slider.GetComponent<Image>().fillAmount = percentage_Activation;
        yield return null;
    }

    public void Start_IconShining(Card.Resource resource)
    {
        currentResource = resource;
        switch (resource)
        {
            case Card.Resource.ACTIVATION:
                actShining.SetBool("finishedSliderUpdating", false);
                actShining.Play("StartShining");
                break;
            case Card.Resource.PHYSICALACT:
                actShining.SetBool("finishedSliderUpdating", false);
                actShining.Play("StartShining");
                break;
            case Card.Resource.FLEXIBILITY:
                flexShining.SetBool("finishedSliderUpdating", false);
                flexShining.Play("StartShining");
                break;
            case Card.Resource.MOTIVATION:
                motShining.SetBool("finishedSliderUpdating", false);
                motShining.Play("StartShining");
                break;
            case Card.Resource.POSITIVE:
                posShining.SetBool("finishedSliderUpdating", false);
                posShining.Play("StartShining");
                break;
            case Card.Resource.NONE:
                break;
        }
    }

    public void Stop_IconShining(Card.Resource resource)
    {
        switch (resource)
        {
            case Card.Resource.ACTIVATION:
                actShining.SetBool("finishedSliderUpdating", true);
                break;
            case Card.Resource.FLEXIBILITY:
                flexShining.SetBool("finishedSliderUpdating", true);
                break;
            case Card.Resource.MOTIVATION:
                motShining.SetBool("finishedSliderUpdating", true);
                break;
            case Card.Resource.POSITIVE:
                posShining.SetBool("finishedSliderUpdating", true);
                break;
            case Card.Resource.NONE:
                break;
        }
    }

    public void updateMaterials_Text(int newMaterialsValue)
    {
        materialsText.text = newMaterialsValue.ToString();
    }

    public void increaseMaterialsOnCanvas(int incremental)
    {
        //Creating text object
        GameObject newText = new GameObject("text", typeof(RectTransform));
        //newText.transform.parent = materialsText.transform;
        newText.transform.SetParent(materialsText.transform);
        newText.transform.name = "incrementalText";
        if(!isMaterialShining)
        {
            materialsShining.Play("StartShining");
            materialsShining.SetBool("finishedSliderUpdating", false);
            StartCoroutine(stopShiningAfterTime(2, materialsShining));
        }
        
        //Setting text configuration
        var newTextComp = newText.AddComponent<Text>();
        newTextComp.text = "+" + incremental.ToString();
        newTextComp.fontSize = 44;
        newTextComp.alignment = TextAnchor.MiddleLeft;
        newTextComp.font = materialsText.font;

        //Setting the position on the canvas
        newText.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
        newText.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
        newText.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
        newText.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
        newText.GetComponent<RectTransform>().localPosition = new Vector3(0, -60, 0);
        newText.AddComponent<incrementMaterialText_Script>();
    }

    IEnumerator stopShiningAfterTime(float timeToGo, Animator animator)
    {
        isMaterialShining = true;
        yield return new WaitForSeconds(timeToGo);
        animator.SetBool("finishedSliderUpdating", true);
        isMaterialShining = false;

        yield return null;
    }

    private void Update()
    {
        //if (SaveObject.Instance.powerR != currentPowerR)
        //{
        //    UpdatePowerRSliderwithAnimation(SaveObject.Instance.powerR);
        //    currentPowerR = SaveObject.Instance.powerR;
        //}
        //if (SaveObject.Instance.energy != currentEnergy)
        //{
        //    UpdateEnergySliderwithAnimation(SaveObject.Instance.energy);
        //    currentEnergy = SaveObject.Instance.powerR;
        //}
    }
}
