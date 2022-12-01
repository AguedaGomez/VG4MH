using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageDecorator : MonoBehaviour
{
    //private PageObject pagesToShow;
    public Text titleText;
    public Text subtitleText;
    public Text contentText;

    public Button exitButton;
    public Button nextButton;
    public Button backButton;

    private int currentPageNum;
    private int currentContentNum;
    private int finalContentNum;
    private int finalPageNum;

    private Content currentContentData;
    private EntryObject currentEntryData;

    private void Awake()
    {
        nextButton.onClick.AddListener(() => StartCoroutine(nextPageCoroutine()));
        backButton.onClick.AddListener(() => StartCoroutine(backPageCoroutine()));
    }

    public void SetUp(EntryObject selectedEntry)
    {
        //pagesToShow = newPage;
        titleText.text = selectedEntry.title;

        currentContentNum = 0;
        currentPageNum = 0;

        currentEntryData = selectedEntry;
        currentContentData = currentEntryData.content[currentContentNum];

        finalContentNum = currentEntryData.content.Count - 1;
        finalPageNum = currentContentData.pages.Count - 1;
        Debug.Log("final num content = " + finalContentNum);
        Debug.Log("final num page = " + finalPageNum);
        UpdateContentVisualization();
        UpdatePageVisualization();
        UpdateButtonVisualization();

        //if (selectedEntry.content.Count == 1)
        //{
        //    nextButton.interactable = false;
        //    backButton.interactable = false;
        //}
        //else
        //{
        //    nextButton.interactable = true;
        //    backButton.interactable = false;
        //}
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
        UpdatePage(1);
        UpdatePageVisualization();
        UpdateButtonVisualization();
        //currentPageNum++;
        //contentText.text = pagesToShow.pageContent[currentPageNum];

        //if (currentPageNum == pagesToShow.pageContent.Length - 1)
        //{
        //    nextButton.interactable = false;
        //}
        //if (!backButton.interactable)
        //{
        //    backButton.interactable = true;
        //}
    }

    public void backPage_ButtonPressed()
    {
        UpdatePage(-1);
        UpdatePageVisualization();
        UpdateButtonVisualization();

        //if(currentPageNum == 0)
        //{
        //    backButton.interactable = false;
        //}
        //if(!nextButton.interactable)
        //{
        //    nextButton.interactable = true;
        //}
    }

    private void UpdatePageVisualization()
    {
        contentText.text = currentContentData.pages[currentPageNum].content;
    }

    private void UpdateContentVisualization()
    {
        currentContentData = currentEntryData.content[currentContentNum];
        subtitleText.text = currentEntryData.content[currentContentNum].title;
    }

    private void UpdatePage(int num)
    {
        currentPageNum = currentPageNum + num;
        Debug.Log(currentPageNum);
        if (currentPageNum > finalPageNum)
        {
            currentPageNum = 0;
            UpdateContent(1);
        }
        else if (currentPageNum < 0)
        {
            UpdateContent(-1);
            currentPageNum = currentContentData.pages.Count - 1;
        }
        
    }

    private void UpdateContent(int num)
    {
        if (currentContentNum > 0 && currentContentNum <= finalContentNum)
        {
            currentContentNum = currentContentNum + num;
            currentContentData = currentEntryData.content[currentContentNum];
            UpdateContentVisualization();
        }
    }

    private void UpdateButtonVisualization()
    {
        if (currentContentNum == finalContentNum && currentPageNum == finalPageNum)
        {
            nextButton.interactable = false;
        }
        if (currentContentNum == 0 && currentPageNum == 0)
        {
            backButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
            backButton.interactable = false;
        }
    }
}
