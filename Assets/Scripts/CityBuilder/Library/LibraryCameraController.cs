using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    [SerializeField] List<Transform> realCameraPositions;
    [SerializeField] List<Camera> auxCameras;
    [SerializeField] GameObject bookObject;
    [SerializeField] GameObject notepadObject;

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



        //Se recalcula el Field Of View de las cámaras para que se ajuste bien lo que se muestra en pantalla
        float notebookCameraAspectRatio = auxCameras[0].aspect;
        int notebookCameraFov = (int)(-103.5714 * notebookCameraAspectRatio + 154.5357f);
        realCameraPositions[0].GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = notebookCameraFov;

        float bookCameraAspectRatio = auxCameras[1].aspect;
        int bookCameraFov = (int)(206.93f - 363.333f * bookCameraAspectRatio + 266.66667f * (Mathf.Pow(bookCameraAspectRatio, 2)));
        realCameraPositions[1].GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = bookCameraFov;
        
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
