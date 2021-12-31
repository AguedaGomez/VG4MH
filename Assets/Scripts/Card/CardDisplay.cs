﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CardDisplay : MonoBehaviour
{
    public TextMeshPro rightOptionText;
    public TextMeshPro leftOptionText;
    public GameObject model; //Prefab del personaje
    public GameObject sceneModel; //el gameObject que hay en la escena para colocar la malla
    public bool startedInitialMovement = false;

    public Card currentCard;
    private float rotationSpeed = 150f;


    //fov perspective 53.2

    protected virtual void Start()
    {
        currentCard = GameManager.Instance.currentCard;  
        DisplayCard();
    }

    public virtual void DisplayCard()
    {
        
    }

    public virtual void ShowOption(Choice.Direction direction)
    {
        
        switch (direction)
        {
            case Choice.Direction.RIGHT:
                rightOptionText.gameObject.SetActive(true);
                leftOptionText.gameObject.SetActive(false);
                break;
            case Choice.Direction.LEFT:
                rightOptionText.gameObject.SetActive(false);
                leftOptionText.gameObject.SetActive(true);
                break;
            case Choice.Direction.NONE:
                rightOptionText.gameObject.SetActive(false);
                leftOptionText.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (startedInitialMovement)
        {
            if (transform.rotation.y > 0)
            {
                transform.RotateAround(transform.position, -transform.up, Time.deltaTime * rotationSpeed);
            }
            else
            {
                Camera.main.orthographic = true;
                startedInitialMovement = false;
            }
        }
    }
}

public interface IDisplay
{
    void DisplayCard();
}
