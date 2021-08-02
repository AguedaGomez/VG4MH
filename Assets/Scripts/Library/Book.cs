using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Book : MonoBehaviour
{
    public string bookTitle;

    public List<string> entries; //en scriptable objectS?
    public TMP_Text titleTextBox;
    public GameObject entry;
    public GameObject index;
    
    // Start is called before the first frame update
    void Start()
    {
        titleTextBox.text = bookTitle;
    }

    public void ShowIndex()
    {
        index.SetActive(true);
        foreach (string e in entries)
        {
            GameObject currentEntry = Instantiate(entry, index.transform);
            currentEntry.GetComponentInChildren<Text>().text = e;
        }
    }

    public void CloseBook()
    {
        //Buscar todos los hijos de index
        //Eliminar los objetos
        //cerrar el contenido del libro
    }
}
