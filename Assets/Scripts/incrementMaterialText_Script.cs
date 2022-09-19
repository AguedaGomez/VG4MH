using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class incrementMaterialText_Script : MonoBehaviour
{
    private void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1), 0.2f).setOnComplete(secondPartOfTheAnimation);
    }

    private void secondPartOfTheAnimation()
    {
        LeanTween.moveLocal(gameObject, new Vector3(0, 0, 0), 2).setOnComplete(DestroyMe);
        LeanTween.alphaText(gameObject.GetComponent<Text>().rectTransform, 0, 1.5f);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
