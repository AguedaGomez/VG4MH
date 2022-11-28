using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryPage_Decorator : MonoBehaviour
{
    private PageObject pagesToShow;
    public Text titleText;
    public Text contentText;

    public Button exitButton;
    public Button nextButton;
    public Button backButton;

    private int actualPage;

    private void Awake()
    {
        nextButton.onClick.AddListener(() => StartCoroutine(nextPageCoroutine()));
        backButton.onClick.AddListener(() => StartCoroutine(backPageCoroutine()));
    }

    public void setUp_HistoryPage(PageObject newPage)
    {
        pagesToShow = newPage;
        titleText.text = pagesToShow.pageTitle;

        actualPage = 0;
        contentText.text = pagesToShow.pageContent[actualPage];
        if (pagesToShow.pageContent.Length == 1)
        {
            nextButton.interactable = false;
            backButton.interactable = false;
        }else
        {
            nextButton.interactable = true;
            backButton.interactable = false;
        }
    }

    public IEnumerator nextPageCoroutine()
    {
        transform.parent.GetComponent<TheoryBookManager>().playPageAnimation();
        yield return new WaitForSeconds(0.3f);
        nextPage_ButtonPressed();
    }

    public IEnumerator backPageCoroutine()
    {
        transform.parent.GetComponent<TheoryBookManager>().playPageAnimation();
        yield return new WaitForSeconds(0.3f);
        backPage_ButtonPressed();
    }

    public void nextPage_ButtonPressed()
    {
        actualPage++;
        contentText.text = pagesToShow.pageContent[actualPage];

        if(actualPage == pagesToShow.pageContent.Length -1)
        {
            nextButton.interactable = false;
        }
        if(!backButton.interactable)
        {
            backButton.interactable = true;
        }
    }

    public void backPage_ButtonPressed()
    {
        actualPage--;
        contentText.text = pagesToShow.pageContent[actualPage];

        if(actualPage == 0)
        {
            backButton.interactable = false;
        }
        if(!nextButton.interactable)
        {
            nextButton.interactable = true;
        }
    }
}
