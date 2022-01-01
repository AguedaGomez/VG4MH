using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizensGenerator : MonoBehaviour
{
    [SerializeField] GameObject standardCitizen;
    [SerializeField] List<GameObject> outstandingCharactersCollection; //En lugar de esto, cada edificio puede tener un Scriptable Object
    [SerializeField]
    [Tooltip("Esta curva permite modificar la relación entre la cantidad de ciudadanos en el juego (X) y la cantidad de ellos que se visualizan (Y)")] 
    AnimationCurve representationCurve;

    List<GameObject> onBoardCitizens = new List<GameObject>();
    List<GameObject> onBoardOutstandingCitizens = new List<GameObject>();

    int totalCitizensInGame = 0;
    int totalCitizensOnBoard = 0;

    private void Awake()
    {
    }

    private void Update()
    {
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

    public void AddCitizens(Building building) 
    {
        totalCitizensInGame += building.nLocals;
        UpdateNumberOfCitizensOnBoard();
    }
    public void RemoveCitizens(Building building)
    {
        totalCitizensInGame -= building.nLocals;
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

                GameObject newCitizen = Instantiate(standardCitizen, randomPoint, Quaternion.identity);
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
    }
}
