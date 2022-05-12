using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Book : MonoBehaviour
{
    [SerializeField] PageController page;
    [SerializeField] Canvas canvas;
    [SerializeField] LibraryCameraController cameraController;

    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextPage(bool hasText = true)
    {
        page.TurnPage();
        if(hasText) text.pageToDisplay++;
    }
    public void BackPage() 
    {
        page.TurnPage();
        text.pageToDisplay--;
    }

    public void HideUI(bool activator) 
    {
        canvas.gameObject.SetActive(activator);
    }
    public void CloseBook() 
    {
        cameraController.TurnToMainView();
    }
}
