using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleChoiceDisplayV2 : CardDisplay, IDisplay
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
        //sceneModel = ((Single)currentCard).sceneModel; //modelo en la escena

        //componentes de modelo escena
        //MeshFilter meshFilterScene = sceneModel.GetComponent<MeshFilter>();
        //MeshRenderer meshRendererScene = sceneModel.GetComponent<MeshRenderer>();

        //componentes del prefab personaje
        MeshFilter meshFilterModel = model.GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRendererModel = model.GetComponentInChildren<MeshRenderer>();

        Debug.Log("a");
        /*meshFilterScene.mesh = meshFilterModel.sharedMesh;
        meshRendererScene.material = meshRendererModel.sharedMaterial;*/
        sceneModel.GetComponent<MeshFilter>().mesh = meshFilterModel.sharedMesh;
        sceneModel.GetComponent<MeshRenderer>().material = meshRendererModel.sharedMaterial;


    }



}
