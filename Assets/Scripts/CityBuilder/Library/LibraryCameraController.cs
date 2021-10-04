using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryCameraController : MonoBehaviour
{

    [SerializeField][Range(0,2)] private int SelectBookKey = 0;
    //[SerializeField][Range(0, 2)] private int ExitBookKey = 1;
    //[SerializeField] private KeyCode NextPageKey = KeyCode.KeypadEnter;
    [SerializeField] private AnimationCurve animationCurve;
    [Space]
    public Animator animator;

    public Camera camera;

    public List<Book> activators;

    private Animation turnPageAnimation;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) 
        {
            animator = transform.GetComponent<Animator>();
        }
        
        foreach (Book book in activators) 
        {
            var canvas = book.GetComponentInChildren<Canvas>();
            if (canvas != null) canvas.worldCamera = camera;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(SelectBookKey))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (animator.GetBool("Close") && Physics.Raycast(ray, out hit))
            {
                CheckHit(hit);
            }
        }
        /*else if (!animator.GetBool("Close") && Input.GetMouseButtonDown(ExitBookKey))
        {
            TurnToMainView();
        }

        if (!animator.GetBool("Close") && Input.GetKeyDown(NextPageKey) && animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Book")) 
        {
            activators[0].TurnPage();
        }
        else if(!animator.GetBool("Close") && Input.GetKeyDown(NextPageKey) && animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Notepad"))
        {
            activators[1].TurnPage();
        }*/
    }

    private void CheckHit(RaycastHit hit)
    {
        if (hit.transform.name == activators[0].transform.name)
        {
            animator.SetBool("Close", false);
            animator.SetBool("OpenBook", true);
        }
        else if (hit.transform.name == activators[1].transform.name)
        {
            animator.SetBool("Close", false);
            animator.SetBool("OpenNotepad", true);
        }
    }

    public void TurnToMainView()
    {
        animator.SetBool("Close", true);
        animator.SetBool("OpenNotepad", false);
        animator.SetBool("OpenBook", false);
    }
}
