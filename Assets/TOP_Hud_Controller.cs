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

    [SerializeField] GameObject power_R_Slider;
    [SerializeField] GameObject activation_Slider;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSliders();
    }

    private void InitializeSliders()
    {
        float powerR = PlayerData.resources[Card.Resource.MOTIVATION];
        float activation = PlayerData.resources[Card.Resource.ACTIVATION];

        float percentage_PowerR = powerR / 100;
        float percentage_Activation = activation / 100;

        power_R_Slider.GetComponent<Image>().fillAmount = percentage_PowerR;
        activation_Slider.GetComponent<Image>().fillAmount = percentage_Activation;
    }

    public void Start_VisualResourceStatChange(Card.Resource resource, int valueModifier)
    {
        if(valueModifier != 0)
        {
            switch (resource)
            {
                case Card.Resource.ACTIVATION:
                    StartPowerRValueUpdate(valueModifier > 0, actShining);
                    StartActValueUpdate(valueModifier > 0);
                    break;
                case Card.Resource.FLEXIBILITY:
                    StartPowerRValueUpdate(valueModifier > 0, flexShining);
                    break;
                case Card.Resource.MOTIVATION:
                    StartPowerRValueUpdate(valueModifier > 0, motShining);
                    break;
                case Card.Resource.POSITIVE:
                    StartPowerRValueUpdate(valueModifier > 0, posShining);
                    break;
                case Card.Resource.NONE:
                    break;
            }
        }
    }

    public void StartPowerRValueUpdate(bool increasing, Animator iconAnimation)
    {
        if (increasing)
        {
            iconAnimation.GetComponent<Image>().color = Color.white;
            var Shape = power_R_Slider.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            Shape.rotation = new Vector3(0, 90, 0);
        }
        else
        {
            iconAnimation.GetComponent<Image>().color = Color.red;
            var Shape = power_R_Slider.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            Shape.rotation = new Vector3(0, -90, 0);
        }


        //Ejecutar la animación correspondiente
        iconAnimation.SetBool("finishedSliderUpdating", false);
        iconAnimation.Play("StartShining");
        StartCoroutine(update_PowerRSliderValue(iconAnimation));
    }

    IEnumerator update_PowerRSliderValue(Animator iconAnimation)
    {
        float percentage_Activation = (float)PlayerData.resources[Card.Resource.MOTIVATION] / 100;
        float prev_Percentage = power_R_Slider.GetComponent<Image>().fillAmount;
        RectTransform edgeRect = power_R_Slider.transform.GetChild(0).GetComponent<RectTransform>();
        ParticleSystem barParticles = power_R_Slider.transform.GetChild(0).GetComponent<ParticleSystem>();
        barParticles.Play();

        float timeToGo = 3f;
        float elapsedTime = 0;

        while (elapsedTime <= timeToGo)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(prev_Percentage, percentage_Activation, (elapsedTime / timeToGo));
            power_R_Slider.GetComponent<Image>().fillAmount = newValue;
            edgeRect.anchorMin = new Vector2(newValue, newValue);
            edgeRect.anchoredPosition = new Vector2(0, 0);

            yield return null;
        }

        barParticles.Stop();
        iconAnimation.SetBool("finishedSliderUpdating", true);
        power_R_Slider.GetComponent<Image>().fillAmount = percentage_Activation;
        yield return null;
    }

    public void StartActValueUpdate(bool increasing)
    {
        if(increasing)
        {
            //iconAnimation.GetComponent<Image>().color = Color.white;
            var Shape = activation_Slider.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            Shape.rotation = new Vector3(0, 90, 0);
        }
        else
        {
            //iconAnimation.GetComponent<Image>().color = Color.red;
            var Shape = activation_Slider.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            Shape.rotation = new Vector3(0, -90, 0);
        }

        //Ejecutar la animación correspondiente
        /*iconAnimation.SetBool("finishedSliderUpdating", false);
        iconAnimation.Play("StartShining");*/
        StartCoroutine(update_ActivationSliderValue());
    }

    IEnumerator update_ActivationSliderValue()
    {
        float percentage_Activation = (float)PlayerData.resources[Card.Resource.ACTIVATION] / 100;
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
        switch (resource)
        {
            case Card.Resource.ACTIVATION:
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
}
