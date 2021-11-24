using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingGraph_Manager : MonoBehaviour
{

    public UI_Grid_Renderer grid;

    public List<UI_LineRenderer> lines; //Reinforcement is in position 0, Endurance is in position 1

    public List<Vector2> reinforcementPoints;
    public List<Vector2> endurancePoints;

    GraphAnimator animator;

    public int boxSize = 50;

    public int maxXValue = 0;
    public int maxYValue = 0;


    private void Start()
    {
        creacionDeListaDeVectores();

        //Llenando de puntos la primera línea
        foreach(Vector2 point in reinforcementPoints)
        {
            if (point.x >= maxXValue)
                maxXValue = (int)point.x;
            
            if (point.y >= maxYValue)
                maxYValue = (int)point.y;

            lines[0].points.Add(point);
        }

        //Llenando de puntos la segunda línea
        foreach (Vector2 point in endurancePoints)
        {
            if (point.x >= maxXValue)
                maxXValue = (int)point.x;

            if (point.y >= maxYValue)
                maxYValue = (int)point.y;

            lines[1].points.Add(point);
        }

        AjusteDeGráfica();

        //Se activa el animador
        animator = GetComponent<GraphAnimator>();
        animator.enabled = true;
    }

    public void AjusteDeGráfica()
    {
        //Se modifica el tamaño del background
        RectTransform parentTransform = GetComponent<RectTransform>();
        parentTransform.sizeDelta = new Vector2(maxXValue * boxSize, 400);

        //Se modifica el tamaño de la máscara para mostrar correctamente la gráfica
        transform.parent.GetComponent<RectTransform>().sizeDelta = parentTransform.sizeDelta;

        //Se modifica el tamaño de la gráfica y se normaliza
        RectTransform childTransform = transform.GetChild(0).GetComponent<RectTransform>();
        childTransform.sizeDelta = parentTransform.sizeDelta;
        
        UI_Grid_Renderer gridManager = transform.GetChild(0).GetComponent<UI_Grid_Renderer>();
        gridManager.enabled = true;
        gridManager.gridSize = new Vector2Int(maxXValue, maxYValue);

        foreach(UI_LineRenderer line in lines)
        {
            line.gridSize = gridManager.gridSize;
        }

        //Activar los LineRenderer para que se acoplen al tamaño de la gráfica
        transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = parentTransform.sizeDelta;
        transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = parentTransform.sizeDelta;
    }

    private void creacionDeListaDeVectores()
    {
        //Rellenando la primera lista
        reinforcementPoints = new List<Vector2>();

        reinforcementPoints.Add(new Vector2(0, 0));
        reinforcementPoints.Add(new Vector2(1, 5));
        reinforcementPoints.Add(new Vector2(2, 4));
        reinforcementPoints.Add(new Vector2(3, 6));
        reinforcementPoints.Add(new Vector2(4, 4));
        reinforcementPoints.Add(new Vector2(5, 3));
        reinforcementPoints.Add(new Vector2(6,2));
        reinforcementPoints.Add(new Vector2(7, 6));
        reinforcementPoints.Add(new Vector2(8, 1));
        reinforcementPoints.Add(new Vector2(9, 7));
        reinforcementPoints.Add(new Vector2(10, 1));
        reinforcementPoints.Add(new Vector2(12, 2));
        reinforcementPoints.Add(new Vector2(16, 6));
        reinforcementPoints.Add(new Vector2(17, 3));
        reinforcementPoints.Add(new Vector2(18, 8));
        reinforcementPoints.Add(new Vector2(19, 1));
        reinforcementPoints.Add(new Vector2(20, 3));
        reinforcementPoints.Add(new Vector2(21, 5));

        //Rellenando la segunda lista
        endurancePoints = new List<Vector2>();

        endurancePoints.Add(new Vector2(0, 0));
        endurancePoints.Add(new Vector2(1, 3));
        endurancePoints.Add(new Vector2(2, 2));
        endurancePoints.Add(new Vector2(3, 3));
        endurancePoints.Add(new Vector2(4, 2));
        endurancePoints.Add(new Vector2(5, 3));
        endurancePoints.Add(new Vector2(6, 1));
        endurancePoints.Add(new Vector2(7, 1));
        endurancePoints.Add(new Vector2(8, 2));
        endurancePoints.Add(new Vector2(9, 6));
        endurancePoints.Add(new Vector2(10, 3));
        endurancePoints.Add(new Vector2(11, 5));
        endurancePoints.Add(new Vector2(12, 2));
        endurancePoints.Add(new Vector2(13, 3));
        endurancePoints.Add(new Vector2(14, 1));
        endurancePoints.Add(new Vector2(15, 3));
        endurancePoints.Add(new Vector2(16, 4));
        endurancePoints.Add(new Vector2(17, 6));
        endurancePoints.Add(new Vector2(18, 5));
        endurancePoints.Add(new Vector2(19, 5));
        endurancePoints.Add(new Vector2(20, 2));
        endurancePoints.Add(new Vector2(21, 3));
    }
}
