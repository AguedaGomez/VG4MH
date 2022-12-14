using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizensGenerator : MonoBehaviour
{
    [SerializeField] GameObject standardCitizen;
    [SerializeField] List<Mesh> standardCitizenMeshes;
    [SerializeField] List<GameObject> outstandingCharactersCollection; //En lugar de esto, cada edificio puede tener un Scriptable Object
    [SerializeField]
    [Tooltip("Esta curva permite modificar la relación entre la cantidad de ciudadanos en el juego (X) y la cantidad de ellos que se visualizan (Y)")]
    AnimationCurve representationCurve;

    List<GameObject> onBoardCitizens = new List<GameObject>();
    List<GameObject> onBoardOutstandingCitizens = new List<GameObject>();

    string[] randomNames = { "Ana" , "Andreas", "Catalina", "Ash", "Bay", "Cri﻿s", "Mel", "Reyes", "Robin", "Gael", "Zoel", "Zuri", "Dani" };
    string[] randomLastnames = { "Miller", "Brown", "Williams", "Jones", "Davis", "Rodriguez", "Clark", "Lewis", "Morris", "Ortiz", "Morgan", "Foster" };

    int totalCitizensInGame = 0;
    int totalCitizensOnBoard = 0;

    private void Awake()
    {
        initializeSavedCitizens();
    }

    private void FixedUpdate()
    {
        selectCitizenToInteract();
    }

    void selectCitizenToInteract()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "local")
                {
                    hit.transform.gameObject.GetComponentInChildren<LocalsMessages>().citizen_OnClick(hit.transform.gameObject);
                }
                else
                {
                }
            }
        }
    }

    /*public void AddCitizens(Building[,] buildings) 
    {

        //Recuenta los ciudadanos que atrae cada casa Building -> nLocals
        for (int i = 0; i < buildings.GetLength(0); i++) 
        {
            for (int j = 0; j < buildings.GetLength(1); j++) 
            {
                totalCitizensInGame += buildings[i, j].nLocals; //Contar ciudadanos normales
                //Si hay algun ciudadano "Destacado" crearlo y guardarlo en la lista OutstandingCitizensList
            }
        }

        UpdateNumberOfCitizensOnBoard();
    }*/
    public void AddSpecialCitizen(GameObject newCharacter)
    {
        totalCitizensInGame++;

        var randomPoint = Board.GetRandomPoint();


        //Se genera un generalCitizen y se implementa la mesh y material correspondiente al especial junto a su nombre para poder ser identificado
        GameObject newCitizen = Instantiate(standardCitizen, randomPoint, Quaternion.identity);
        newCitizen.transform.GetChild(0).GetComponent<MeshFilter>().mesh = newCharacter.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
        newCitizen.transform.GetChild(0).GetComponent<MeshRenderer>().material = newCharacter.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
        newCitizen.transform.GetChild(0).gameObject.GetComponent<Local>().localName = newCharacter.transform.GetChild(0).gameObject.GetComponent<Local>().localName;
        newCitizen.transform.GetChild(0).gameObject.GetComponent<LocalsMessages>().isSpecialCitizen = true;

        onBoardCitizens.Add(newCitizen);
        updateStoredCharacters();
    }

    public void AddCitizens(Building building)
    {
        totalCitizensInGame += building.GetNLocals();
        UpdateNumberOfCitizensOnBoard();
    }
    public void RemoveCitizens(Building building)
    {
        totalCitizensInGame -= building.GetNLocals();
        UpdateNumberOfCitizensOnBoard();
    }

    private void UpdateNumberOfCitizensOnBoard()
    {
        //Obtener la cantidad de ciudadanos representados en el tablero en función del total de ciudadanos del juego
        int nCitizensOnboard = (int)representationCurve.Evaluate(totalCitizensInGame);

        int CitizensGap = nCitizensOnboard - totalCitizensOnBoard;
        totalCitizensOnBoard = nCitizensOnboard;

        //Debug.Log(nameof(totalCitizensInGame) +" "+ totalCitizensInGame);
        //Debug.Log(nameof(totalCitizensOnBoard) +" "+ totalCitizensOnBoard);

        if (CitizensGap >= 0)
        {
            for (int i = 0; i < CitizensGap; i++)
            {
                var randomPoint = Board.GetRandomPoint();

                //Se instancia el ciudadano general
                GameObject newCitizen = Instantiate(standardCitizen, randomPoint, Quaternion.identity);

                //Se decide que mesh(forma) tendrá el ciudadano y se intercambia por la general
                Mesh citizenRandomMesh = standardCitizenMeshes[UnityEngine.Random.Range(0, standardCitizenMeshes.Count)];
                newCitizen.transform.GetChild(0).GetComponent<MeshFilter>().mesh = citizenRandomMesh;

                //Se le setea un nuevo nombre aleatorio
                int indexName = UnityEngine.Random.Range(0, randomNames.Length);
                int indexLastame = UnityEngine.Random.Range(0, randomLastnames.Length);
                string newCitizenName = randomNames[indexName] + " " + randomLastnames[indexLastame];
                newCitizen.transform.GetChild(0).gameObject.GetComponent<Local>().localName = newCitizenName;
                //newCitizen.transform.GetChild(0).gameObject.GetComponent<LocalsMessages>().specialCitizenWithConflict = false;

                onBoardCitizens.Add(newCitizen);
            }
        }
        else
        {
            for (int i = 0; i < -CitizensGap; i++)
            {
                var citizen = onBoardCitizens[i];
                onBoardCitizens.RemoveAt(i);
                Destroy(citizen);
            }
        }
        updateStoredCharacters();
    }

    private void updateStoredCharacters()
    {
        List<CharacterInfo> listToUpdate = new List<CharacterInfo>();

        foreach (GameObject character in onBoardCitizens)
        {
            CharacterInfo infoToAdd = new CharacterInfo();
            Local citizenInfo = character.GetComponentInChildren<Local>();

            infoToAdd.characterName = citizenInfo.localName;
            infoToAdd.modelMesh = character.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
            infoToAdd.modelMaterial = character.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
            infoToAdd.specialCitizen = character.transform.GetChild(0).gameObject.GetComponent<LocalsMessages>().isSpecialCitizen;

            listToUpdate.Add(infoToAdd);
        }
        SaveObject.Instance.charactersInTheCity = listToUpdate;
        GameManager.Instance.checkCurrentCards(onBoardCitizens);
    }

    private void initializeSavedCitizens()
    {
        if(SaveObject.Instance.charactersInTheCity.Count > 0)
        {
            foreach(CharacterInfo charInfo in SaveObject.Instance.charactersInTheCity)
            {
                var randomPoint = Board.GetRandomPoint();
                GameObject newCitizen = Instantiate(standardCitizen, randomPoint, Quaternion.identity);

                newCitizen.GetComponentInChildren<Local>().localName = charInfo.characterName;
                newCitizen.transform.GetChild(0).GetComponent<MeshFilter>().mesh = charInfo.modelMesh;
                newCitizen.transform.GetChild(0).GetComponent<MeshRenderer>().material = charInfo.modelMaterial;
                newCitizen.transform.GetChild(0).gameObject.GetComponent<LocalsMessages>().isSpecialCitizen = charInfo.specialCitizen;

                onBoardCitizens.Add(newCitizen);
            }
            GameManager.Instance.checkCurrentCards(onBoardCitizens);
        }
    }
}



