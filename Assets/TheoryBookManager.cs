using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoryBookManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu_gameObject;
    [SerializeField] GameObject historyPage_gameObject;
    [SerializeField] GameObject mainMenu_Viewport_gameobject;
    [SerializeField] GameObject historyOption_prefab;
    [SerializeField] PageController theoryBookPageController;

    [SerializeField] List<PageObject> bookEntries;

    private void Start()
    {
        InitializePanel();
    }

    private void InitializePanel()
    {
        foreach(PageObject newPage in bookEntries)
        {
            if(newPage.storyUnlocked)
            {
                GameObject newPageObject = (GameObject)Instantiate(historyOption_prefab, mainMenu_Viewport_gameobject.transform);
                newPageObject.GetComponent<StoryOption_Decorator>().setUp_StoryOption(newPage);
                newPageObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => StartCoroutine(storyOption_OnClick(newPage)));
            }
        }

        historyPage_gameObject.GetComponent<HistoryPage_Decorator>().exitButton.onClick.AddListener(() => StartCoroutine(returnToMainMenu()));
            }

    public IEnumerator storyOption_OnClick(PageObject storyToShow)
    {
        theoryBookPageController.turnPageAnimationStart();
        yield return new WaitForSeconds(0.3f);
        
        storyToShow.viewedByUser = true;
        historyPage_gameObject.SetActive(true);
        historyPage_gameObject.GetComponent<HistoryPage_Decorator>().setUp_HistoryPage(storyToShow); //Dar el contenido asociado 
        mainMenu_gameObject.SetActive(false);
    }

    public IEnumerator returnToMainMenu()
    {
        theoryBookPageController.turnPageAnimationStart();
        yield return new WaitForSeconds(0.3f);

        historyPage_gameObject.SetActive(false);
        mainMenu_gameObject.SetActive(true);
        reInitializePanel();
    }

    public void playPageAnimation()
    {
        theoryBookPageController.turnPageAnimationStart();
    }

    private void reInitializePanel()
    {
        foreach(Transform child in mainMenu_Viewport_gameobject.transform)
        {
            Destroy(child.gameObject);
        }
        InitializePanel();
    }
}
