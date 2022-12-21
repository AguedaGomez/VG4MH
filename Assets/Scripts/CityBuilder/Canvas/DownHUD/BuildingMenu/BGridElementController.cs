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

    private Construction bData;
    private Button buildButton;

    void Start()
    {
        bData = GameManager.Instance.buildingsInGame[myId];
        buildButton = transform.Find("Build").GetComponent<Button>();
    }

    private void SetUnlockData()
    {
        bData = GameManager.Instance.buildingsInGame[myId];

        transform.Find("Name").GetComponent<Text>().text = bData.buildingName;
        transform.Find("Image").GetComponent<Image>().sprite = bData.image;
        transform.Find("Image").GetComponent<Image>().SetNativeSize();
        //transform.Find("Mat Number").GetComponent<Text>().text = "" + construction.maximunMaterials; <-Revisar esto
        
        Transform buildButtonTransform = transform.Find("Build");
        buildButtonTransform.transform.Find("Lock icon").gameObject.SetActive(false);
        buildButtonTransform.GetComponent<Button>().interactable = true;

        Text buildText = transform.Find("Build").transform.Find("Text").GetComponent<Text>();
        buildText.text = bData.cost + "";
        buildText.gameObject.SetActive(true);

        transform.Find("Description").GetComponent<Text>().text = bData.description;

        transform.Find("GridNumber").GetComponent<Text>().text = "" + bData.cellsInX + "x" + bData.cellsInZ;
    }

    private void SetLockData()
    {
        Construction bData = GameManager.Instance.buildingsInGame[myId];

        transform.Find("Name").GetComponent<Text>().text = "??????";
        transform.Find("Image").GetComponent<Image>().sprite = bData.silhouette;
        transform.Find("Image").GetComponent<Image>().SetNativeSize();
        transform.Find("Mat Number").GetComponent<Text>().text = "??";

        Transform buildButtonTransform = transform.Find("Build");
        buildButtonTransform.transform.Find("Lock icon").gameObject.SetActive(true);
        buildButtonTransform.GetComponent<Button>().interactable = false;

        transform.Find("Description").GetComponent<Text>().text = bData.description;

        transform.Find("GridNumber").GetComponent<Text>().text = "????";
    }
    public void Lock()
    {
        SetLockData();
    }  
    public void UnLock()
    {
        SetUnlockData();
    }

    public void ShowMaterialsMssg()
    {
        transform.Find("MaterialMssg").gameObject.SetActive(true);
        StartCoroutine(ShowErrorMessage());
    }

    private IEnumerator ShowErrorMessage()
    {
        yield return new WaitForSeconds(1f);
        transform.Find("MaterialMssg").gameObject.SetActive(false);
    }
}
