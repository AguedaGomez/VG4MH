using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Card currentCard;

    public Construction buildingInConstruction; //datos del edificio que se quiere construir

    public bool interactingWithUI = false;

    public List<Construction> buildingsInGameList = new List<Construction>();
    public List<ActivityNotification> goalsToReach = new List<ActivityNotification>();
    public Dictionary<string, Construction> buildingsInGame= new Dictionary<string, Construction>();

    private GameObject city;
    private SaveAndLoadDataManager saveAndLoadDataManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CreateDictionaryBuildingsInGame();
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

    private void CreateDictionaryBuildingsInGame()
    {
        Debug.Log("Creando diccionario de edificios en el juego");
        foreach (Construction construction in buildingsInGameList)
        {
            buildingsInGame.Add(construction.id, construction);
        }
    }

    internal void resetActivityNotifications()
    {
        foreach (ActivityNotification noti in goalsToReach)
        {
            noti.hasBeenShown = false;
        }
    }
}
