using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour

{
    public void Hide()
    {
        gameObject.SetActive(false);
        if (gameObject.GetComponentInParent<ShowAndHide>())
            gameObject.GetComponentInParent<ShowAndHide>().Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);

    }

}