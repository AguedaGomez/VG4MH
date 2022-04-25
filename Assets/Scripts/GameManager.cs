using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Card currentCard;

    public bool interactingWithUI = false;

    public List<Construction> buildingsInGame = new List<Construction>();

    private GameObject city;
    private SaveAndLoadDataManager saveAndLoadDataManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        //FindCity();
        //saveAndLoadDataManager.SaveGame();
        gameObject.GetComponent<SaveAndLoadDataManager>().SaveGame();
    }

    public void LoadGame()
    {
        //SceneManager.LoadScene("CityBuilder");
        StartCoroutine(LoadYourAsyncScene());
        gameObject.GetComponent<SaveAndLoadDataManager>().LoadGame();

    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CityBuilder");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
