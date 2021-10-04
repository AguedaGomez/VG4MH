using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] private Animation turnPageAnimation;
    [SerializeField] private Book book;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TurnPage()
    {
        turnPageAnimation.Play();
    }

    public void HideUI()
    {
        book.HideUI(false);
    }
    public void UnhideUI()
    {
        book.HideUI(true);
    }
}
