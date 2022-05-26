﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleChoiceDisplay : CardDisplay, IDisplay
{
    // Start is called before the first frame update
    public override void DisplayCard()
    {
        // preparing camera for initial movement of card
        Camera.main.orthographic = false;
        startedInitialMovement = true;

        // preparing card data
        rightOptionText.text = ((Single)currentCard).rightText;
        leftOptionText.text = ((Single)currentCard).leftText;
        model = ((Single)currentCard).model; //modelo 3D prefab personaje
        sceneModel = sceneModel = GameObject.Find("/CharacterModel"); //modelo en la escena

        //componentes del prefab personaje
        MeshFilter meshFilterModel = model.GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRendererModel = model.GetComponentInChildren<MeshRenderer>();

        sceneModel.GetComponent<MeshFilter>().mesh = meshFilterModel.sharedMesh;
        sceneModel.GetComponent<MeshRenderer>().material = meshRendererModel.sharedMaterial;
    }



}
