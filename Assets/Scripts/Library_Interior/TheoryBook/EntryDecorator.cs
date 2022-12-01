using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryDecorator : MonoBehaviour
{
    [SerializeField] Text title_Text;
    [SerializeField] GameObject newStory_Image;
    //public PageObject referencedStoryPage;

    public void SetUp(EntryObject entryToShow)
    {
        title_Text.text = entryToShow.title;
        newStory_Image.SetActive(!entryToShow.read);
        //referencedStoryPage = pageToShow;
    }
}
