using System.Collections;
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
        rightOptionText.text = ((Single)currentCard).choice.rightText;
        leftOptionText.text = ((Single)currentCard).choice.leftText;
        model = ((Single)currentCard).choice.model; //modelo 3D prefab personaje
        sceneModel = ((Single)currentCard).choice.sceneModel; //modelo en la escena

        //componentes de modelo escena
        MeshFilter meshFilterScene = sceneModel.GetComponent<MeshFilter>();
        MeshRenderer meshRendererScene = sceneModel.GetComponent<MeshRenderer>();

        //componentes del prefab personaje
        MeshFilter meshFilterModel = model.GetComponent<MeshFilter>();
        MeshRenderer meshRendererModel = model.GetComponent<MeshRenderer>();

        Debug.Log("a");
        meshFilterScene.mesh = meshFilterModel.sharedMesh;
        meshRendererScene.material = meshRendererModel.sharedMaterial;




    }



}
