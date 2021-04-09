using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowPopUpConflict : MonoBehaviour
{
    public GameObject popUp;

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
