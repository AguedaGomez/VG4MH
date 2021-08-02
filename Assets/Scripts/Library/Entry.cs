using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Entry : MonoBehaviour
{
    private TMP_Text titleTextBox;
    private TMP_Text bodyTextBox;
    private GameObject content;

    public string title; //en scriptable object?
    public string body;
    void Start()
    {
        content = GameObject.Find("Book Content");
        titleTextBox = content.transform.Find("Title").GetComponentInChildren<TMP_Text>();
        bodyTextBox = content.transform.Find("Body").GetComponentInChildren<TMP_Text>();
    }

    public void GoEntryContent()
    {
        titleTextBox.text = title;
        bodyTextBox.text = body;

        bodyTextBox.gameObject.SetActive(true);
        content.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
