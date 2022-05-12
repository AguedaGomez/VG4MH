using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGridElementController : MonoBehaviour
{
    public string myId;
    public enum State
    {
        LOCK,
        UNLOCK
        //NEW?
    }
    public State myState;

    void Start()
    {
        
    }

    private void SetUnlockData()
    {
        Construction construction = GameManager.Instance.buildingsInGame[myId];

        transform.Find("Name").GetComponent<Text>().text = construction.buildingName;
        transform.Find("Image").GetComponent<Image>().sprite = construction.image;
        transform.Find("Mat Number").GetComponent<Text>().text = "" + construction.maximunMaterials;
        
        Transform buildButtonTransform = transform.Find("Build");
        buildButtonTransform.transform.Find("Lock icon").gameObject.SetActive(false);
        buildButtonTransform.GetComponent<Button>().interactable = true;

        Text buildText = transform.Find("Build").transform.Find("Text").GetComponent<Text>();
        buildText.text = "" + construction.cost;
        buildText.gameObject.SetActive(true);

        transform.Find("GridNumber").GetComponent<Text>().text = "" + construction.cellsInX + "x" + construction.cellsInZ;
    }

    private void SetLockData()
    {
        Construction construction = GameManager.Instance.buildingsInGame[myId];

        transform.Find("Name").GetComponent<Text>().text = "??????";
        transform.Find("Image").GetComponent<Image>().sprite = construction.silhouette;
        transform.Find("Mat Number").GetComponent<Text>().text = "??";

        Transform buildButtonTransform = transform.Find("Build");
        buildButtonTransform.transform.Find("Lock icon").gameObject.SetActive(true);
        buildButtonTransform.GetComponent<Button>().interactable = false;

        transform.Find("GridNumber").GetComponent<Text>().text = "????";
    }
    public void Lock()
    {
        Debug.Log("Lock building");
        SetLockData();
    }  
    public void UnLock()
    {
        SetUnlockData();
    }

}
