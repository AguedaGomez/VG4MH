using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryOption_Decorator : MonoBehaviour
{
    [SerializeField] Text title_Text;
    [SerializeField] GameObject newStory_Image;
    public PageObject referencedStoryPage;

    public void setUp_StoryOption(PageObject pageToShow)
    {
        title_Text.text = pageToShow.pageTitle;
        newStory_Image.SetActive(!pageToShow.viewedByUser);
        referencedStoryPage = pageToShow;
    }
}
