using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheoryBookManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu_gameObject;
    [SerializeField] GameObject bookPage;
    [SerializeField] GameObject mainMenu_Viewport_gameobject;
    [SerializeField] GameObject entryPrefab;
    [SerializeField] PageController theoryBookPageController;

    //[SerializeField] List<PageObject> bookEntries;
    [SerializeField] List<EntryObject> bookEntries;

    private void Start()
    {
        InitializeBookEntries();
    }

    private void InitializeBookEntries()
    {
        foreach(EntryObject entry in bookEntries)
        {
            //Descomentar para probar toda la secuencia
            //if (GameManager.Instance.checkPointsStory.Contains(entry.typeContent))
            //{
            //    entry.unlocked = true;
            //}
            if(entry.unlocked)
            {
                GameObject entryObject = Instantiate(entryPrefab, mainMenu_Viewport_gameobject.transform);
                entryObject.GetComponent<EntryDecorator>().SetUp(entry);
                Button entryButton = entryObject.GetComponent<Button>();
                entryButton.onClick.AddListener(() => StartCoroutine(EntryOnClick(entry)));
            }
        }

        bookPage.GetComponent<PageDecorator>().exitButton.onClick.AddListener(() => StartCoroutine(returnToMainMenu()));
    }

    public IEnumerator EntryOnClick(EntryObject entryToShowContent)
    {
        theoryBookPageController.turnPageAnimationStart();
        yield return new WaitForSeconds(0.3f);

        entryToShowContent.read = true;
        bookPage.SetActive(true);
        bookPage.GetComponent<PageDecorator>().SetUp(entryToShowContent);
        mainMenu_gameObject.SetActive(false);
    }

    public IEnumerator returnToMainMenu()
    {
        theoryBookPageController.turnPageAnimationStart();
        yield return new WaitForSeconds(0.3f);

        bookPage.SetActive(false);
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
        InitializeBookEntries();
    }
}
