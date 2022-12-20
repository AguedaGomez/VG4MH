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
    private string totalNumPages;
    private int globalNumPage;

    private Content currentContentData;
    private EntryObject currentEntryData;

    private Text paginator;

    private void Awake()
    {
        nextButton.onClick.AddListener(() => StartCoroutine(nextPageCoroutine()));
        backButton.onClick.AddListener(() => StartCoroutine(backPageCoroutine()));

        paginator = transform.Find("Paginator").GetComponent<Text>();
    }

    public void SetUp(EntryObject selectedEntry)
    {
        //pagesToShow = newPage;
        titleText.text = selectedEntry.title;

        currentContentNum = 0;
        currentPageNum = 0;

        currentEntryData = selectedEntry;
        currentContentData = currentEntryData.content[currentContentNum];

        //finalContentNum = currentEntryData.content.Count == 1 ? currentEntryData.content.Count : currentEntryData.content.Count - 1;
        //finalPageNum = currentContentData.pages.Count == 1 ? currentContentData.pages.Count : currentContentData.pages.Count - 1;

        finalContentNum = currentEntryData.content.Count - 1;
        finalPageNum = currentContentData.pages.Count - 1;
        totalNumPages = SetPaginator();

        UpdatePaginator();
        UpdateContentVisualization();
        UpdatePageVisualization();
        nextButton.interactable = true;
        backButton.interactable = true;
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
        UpdatePaginator();
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
        UpdatePaginator();

        //if(currentPageNum == 0)
        //{
        //    backButton.interactable = false;
        //}
        //if(!nextButton.interactable)
        //{
        //    nextButton.interactable = true;
        //}
    }

    private void UpdatePaginator()
    {
        paginator.text = globalNumPage + "/" + totalNumPages;
    }

    private string SetPaginator()
    {
        globalNumPage = 1;
        int totalPages = 0;
        foreach (var content in currentEntryData.content)
        {
            foreach (var page in content.pages)
            {
                totalPages++;
            }
        }
        return "" + totalPages;
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
        globalNumPage += num;
        if (currentPageNum > finalPageNum)
        {
            currentPageNum = 0;
            UpdateContent(1);
            finalPageNum = currentContentData.pages.Count - 1;
        }
        else if (currentPageNum < 0)
        {
            UpdateContent(-1);
            finalPageNum = currentContentData.pages.Count - 1;
            currentPageNum = finalPageNum;
        }
        

    }

    private void UpdateContent(int num)
    {
        if (currentContentNum >= 0 && currentContentNum <= finalContentNum)
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
        else if (currentContentNum == 0 && currentPageNum == 0)
        {
            backButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
            backButton.interactable = true;
        }
    }
}
