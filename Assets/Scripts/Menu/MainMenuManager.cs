using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    public InputField idInputField;
    public Card tutorial;

    private string ID = "123456";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Login()
    {
        
    }
    public void StartGame ()
    {
        if (SaveObject.Instance.date == "")
        {
            GameManager.Instance.currentCard = tutorial;

            SceneManager.LoadScene("DecisionMakingGame");
        } else
        {
            //SceneManager.LoadScene("CityBuilder");
            GameManager.Instance.SaveGame();
            GameManager.Instance.LoadGame();
        }
    }
}
