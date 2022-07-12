using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryPage_Decorator : MonoBehaviour
{
    public Text titleText;
    public Text contentText;

    public Button exitButton;

    public void setUp_HistoryPage(PageObject newPage)
    {
        titleText.text = newPage.pageTitle;
        contentText.text = newPage.pageContent;
    }
}
