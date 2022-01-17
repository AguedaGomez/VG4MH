using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowAndHideMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
        GameManager.Instance.interactingWithUI = menu.activeSelf;
    }
}
