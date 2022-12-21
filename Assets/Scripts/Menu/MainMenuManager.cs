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

    private string ID = "1234";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Login()
    {
        
    }
    public void StartGame ()
    {
        if (idInputField.text == ID)
        {
            if (SaveObject.Instance.firstTimeInGame)
            {
                GameManager.Instance.currentCard = tutorial;
                SaveObject.Instance.currentScene = "DecisionMakingGame";
                SaveObject.Instance.firstTimeInGame = false;
                GameManager.Instance.SaveGame();

                SceneManager.LoadScene(SaveObject.Instance.currentScene);
            }
            else
            {
                SaveObject.Instance.currentScene = "CityBuilder";
                GameManager.Instance.SaveGame();
                GameManager.Instance.LoadGame();
            }
        }

    }
}
