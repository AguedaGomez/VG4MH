using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConflictManager : MonoBehaviour
{
    public ShowPopUpConflict showPopUpConflict;
    // Start is called before the first frame update
    void Start()
    {
        LaunchConflict();
    }

    private void LaunchConflict()
    {
        Card currentConflict = GameManager.Instance.currentCard;
        GameObject pj = transform.Find(currentConflict.characterName).gameObject;
        
        Local local = pj.GetComponent<Local>();
        showPopUpConflict.FillPopUp(local.localImage, local.localName);
        
        LocalsMessages messageScript = pj.GetComponent<LocalsMessages>();
        messageScript.ShowConflictExclamation();
        
    }
}
