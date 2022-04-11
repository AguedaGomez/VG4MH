using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el comportamiento del menú inferior
/// cuándo aparecen y desaparecen
/// </summary>
public class DownHudController : MonoBehaviour
{
    private GameObject activeSubmenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //public void InteractingWithUI()
    //{
    //    GameManager.Instance.interactingWithUI = true;
    //}

    public void ShowAndHideActionsMenu(GameObject panel)
    {

        if (panel.activeSelf)
        {
            if (activeSubmenu != null)
            {
                activeSubmenu.SetActive(false);
                activeSubmenu = null;
            }
            panel.SetActive(false);
        }
        else
            panel.SetActive(true);
        

        //GameManager.Instance.interactingWithUI = menu.activeSelf; //comprobar si esto funciona despues de modificar el on value changed d building menu
    }

    public void ShowAndHideSubmenu(GameObject menu)
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            activeSubmenu = null;
        }
            
        else
        {
            if (activeSubmenu != null && activeSubmenu != menu)
            {

                activeSubmenu.SetActive(false);
                menu.SetActive(true);
                activeSubmenu = menu;
            }
            else
            {
                menu.SetActive(true);
                activeSubmenu = menu;
            }

        }
    }


}
