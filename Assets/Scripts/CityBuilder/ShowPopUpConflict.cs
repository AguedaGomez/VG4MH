using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowPopUpConflict : MonoBehaviour
{
    public GameObject popUp;
    public Image pjImage;
    public Text pjName;

    public void FillPopUp(Sprite pjSprite, string name)
    {
        pjImage.sprite = pjSprite;
        pjName.text = name;
    }
    public void ShowPopUp()
    {
        //mirar qué conflicto debería mostrarse
        popUp.SetActive(true); //thistransform
    }

    public void YesButton()
    {
        SceneManager.LoadScene("DecisionMakingGame");
    }
}
