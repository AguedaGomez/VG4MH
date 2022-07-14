using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Book : MonoBehaviour
{
    [SerializeField] PageController page;
    [SerializeField] Canvas PHQ_canvas;
    [SerializeField] Canvas GAD_canvas;
    [SerializeField] LibraryCameraController cameraController;

    TextMeshProUGUI text;
    bool showingPHQ_Canvas = true;

    [SerializeField] Text postIt_Text;
    [SerializeField] Text[] postIt_Texts;
    [SerializeField] Image[] postIt_Images;

    [SerializeField] private Animation postItAnimation;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    
    public void NextPHQ_Page(bool hasText = true)
    {
        page.TurnPHQ_Page();
        if(hasText) text.pageToDisplay++;
    }
    public void BackPHQ_Page() 
    {
        page.TurnPHQ_Page();
        text.pageToDisplay--;
    }
    public void Hide_UI(bool activator) 
    {
        if(showingPHQ_Canvas)
        {
            PHQ_canvas.gameObject.SetActive(activator);
        }
        else
        {
            GAD_canvas.gameObject.SetActive(activator);
        }
    }

    public void NextGAD_Page(bool hasText = true)
    {
        page.TurnGAD_Page();
        if (hasText) text.pageToDisplay++;
    }
    public void BackGAD_Page()
    {
        page.TurnGAD_Page();
        text.pageToDisplay--;
    }
    public void HideGAD_UI(bool activator)
    {
        GAD_canvas.gameObject.SetActive(activator);
    }

    public void CloseBook() 
    {
        cameraController.TurnToMainView();
    }

    public void changeGraphic()
    {
        postItAnimation.Play();
        Color colorChanger;

        if (PHQ_canvas.gameObject.active)
        {
            PHQ_canvas.gameObject.SetActive(false);
            //GAD_canvas.gameObject.SetActive(true);
            showingPHQ_Canvas = false;
            postIt_Text.text = "Mostrando tabla GAD";


            postIt_Texts[0].text = "Valor GAD 1";
            ColorUtility.TryParseHtmlString("#B500FF", out colorChanger);
            postIt_Images[0].color = colorChanger;

            postIt_Texts[1].text = "Valor GAD 2";
            ColorUtility.TryParseHtmlString("#6400FF", out colorChanger);
            postIt_Images[1].color = colorChanger;

            postIt_Texts[2].text = "Valor Activación";
            ColorUtility.TryParseHtmlString("#08C886", out colorChanger);
            postIt_Images[2].color = colorChanger;

        }
        else
        {
            //PHQ_canvas.gameObject.SetActive(true);
            GAD_canvas.gameObject.SetActive(false);
            showingPHQ_Canvas = true;
            postIt_Text.text = "Mostrando tabla PHQ";

            postIt_Texts[0].text = "Valor PHQ 1";
            ColorUtility.TryParseHtmlString("#FF0000", out colorChanger);

            postIt_Images[0].color = colorChanger;

            postIt_Texts[1].text = "Valor PHQ 2";
            ColorUtility.TryParseHtmlString("#FF007F", out colorChanger);

            postIt_Images[1].color = colorChanger;

            postIt_Texts[2].text = "Valor Activación";
            ColorUtility.TryParseHtmlString("#08C886", out colorChanger);

            postIt_Images[2].color = colorChanger;
        }
    }
}
