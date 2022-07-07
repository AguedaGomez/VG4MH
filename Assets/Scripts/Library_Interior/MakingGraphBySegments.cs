using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MakingGraphBySegments : MonoBehaviour
{
    //Referencias a la malla, texto y las dos l�neas de la gr�fica
    public UI_Grid_Renderer grid;
    public TMP_Text textToShow;
    public List<UI_LineRenderer> lines; //Reinforcement is in position 0, Endurance is in position 1

    //Listas donde se van a guardar todos los puntos de la l�nea de la gr�fica, en la l�nea en s� solo se almacenar�n los puntos "activos" en ese momento
    public List<Vector2> firstListOfPoints;
    public List<Vector2> endurancePoints;
    [SerializeField] GameObject indexText_Prefab;

    public Button nextPage_Button;
    public Button previousPage_Button;

    //Este es el n�mero total de puntos a mostrar (reinforcementPoints) partido de los puntos que se pueden mostrar a la vez en pantalla en la UI_LineRenderer
    //Si la divisi�n no da exacta se guardar� el valor "resto" para saber cuantos valores mostrar en la �ltima pantalla
    public int numeroDeSegmentos;
    public int resto;
    
    //Indica si la cantidad de puntos son exactos o habr� una p�gina final de puntos parcial
    bool endingGraphAvailable = false;
    public int actualPage = -1; //Si est� en el valor -1 es porque est� en la �ltima p�gina (p�gina que resulta del resto)

    public string idLine_1;
    public string idLine_2;

    //Componente que anima la gr�fica
    GraphAnimator animator;


    void Start()
    {
        //Funci�n debug para crear la lista de vectores que posteriormente se mostrar�
        creacionDeListaDeVectores();

        //Se determina cuantos segmentos de valores van a haber y si va a haber p�gina parcial
        numeroDeSegmentos = firstListOfPoints.Count / grid.gridSize.x;
        resto = firstListOfPoints.Count % grid.gridSize.x;

        if(resto != 0)
        {
            endingGraphAvailable = true;
        }

        //Finalmente dibuja la gr�fica
        DibujarTablaForFirstTime();
    }

    private void DibujarTablaForFirstTime()
    {
        //Si hay p�gina parcial entonces...
        if(endingGraphAvailable)
        {
            for(int i = 0; i < resto; i++)
            {
                //Se cogen los valores correspondientes a la �ltima p�gina
                Vector2 reinforcementPoint = new Vector2(i, firstListOfPoints[(firstListOfPoints.Count - resto) + i].y);
                Vector2 endurancePoint = new Vector2(i, endurancePoints[(endurancePoints.Count - resto) + i].y);

                //Se a�aden los valores al componente UI_LineRenderer
                lines[0].points.Add(reinforcementPoint);
                lines[1].points.Add(endurancePoint);

                actualPage = -1;

                //Se indica al texto UI en qu� p�gina se est�
                actualizarTextoDeInterfaz(numeroDeSegmentos + 1, numeroDeSegmentos + 1);
            }
        }
        //En caso de que no haya p�gina parcial y la �ltima p�gina vaya a estar completa de valores
        else
        {
            for(int i = 0; i < 7; i++)
            {
                int index = i + (7 * (numeroDeSegmentos - 1));

                Vector2 reinforcementPoint = new Vector2(i, firstListOfPoints[index].y);
                Vector2 endurancePoint = new Vector2(i, endurancePoints[index].y);

                lines[0].points.Add(reinforcementPoint);
                lines[1].points.Add(endurancePoint);

                //En este caso no va a haber p�gina parcial en toda la traza por lo que la p�gina final ser� la misma a n�mero de segmentos haya
                actualPage = numeroDeSegmentos;
                actualizarTextoDeInterfaz(numeroDeSegmentos, numeroDeSegmentos);
            }
        }

        Vector2 gridSize = grid.GetComponent<RectTransform>().rect.size;
        double xSpaceBetweenNumbers = (double)(gridSize.x / grid.gridSize.x);
        double ySpaceBetweenNumbers = (double)(gridSize.y / grid.gridSize.y);
        CrearNumeracionTabla(xSpaceBetweenNumbers, ySpaceBetweenNumbers);

        //Al ser la �ltima p�gina el bot�n de siguiente p�gina estar� oculto
        nextPage_Button.interactable = false;

        //Si la cantidad de n�meros no da para m�s de una p�gina...
        if (numeroDeSegmentos == 0 || (numeroDeSegmentos == 1 && resto == 0))
        {
            previousPage_Button.interactable = false;
        }

        //Se activa el animador
        animator = GetComponent<GraphAnimator>();
        animator.enabled = true;

    }

    private void CrearNumeracionTabla(double cellHeight, double cellWidth)
    {
        /*float anchura = grid.getCellWidth();
        float altura = grid.getCellHeight();*/

        for (int i = 0; i < grid.gridSize.x+1; i++)
        {
            for(int j = 0; j < grid.gridSize.y+1; j++)
            {
                if(i == 0)
                {
                    //Se analiza el caso del (0,0)
                    if(j == 0)
                    {
                        Vector2 initialPosition = new Vector2(0, 0);
                        GameObject initialText = Instantiate(indexText_Prefab, transform);
                        initialText.name = "NewText_" + i + "_" + j;
                        initialText.GetComponent<Text>().text = i.ToString() + " , " + j.ToString();
                        initialText.transform.parent = transform;
                        initialText.transform.localScale = new Vector3(4, 4, 4);
                        initialText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
                        continue;
                    }

                    GameObject newText = Instantiate(indexText_Prefab, transform);
                    newText.name = "NewText_" + i + "_" + j;
                    newText.GetComponent<Text>().text = j.ToString();
                    newText.transform.parent = transform;
                    newText.transform.localScale = new Vector3(4, 4, 4);
                    newText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, j *(float)cellWidth);
                }
                if(j == 0)
                {
                    GameObject newText = Instantiate(indexText_Prefab, transform);
                    newText.name = "NewText_" + i + "_" + j;
                    newText.transform.localScale = new Vector3(4, 4, 4);
                    newText.GetComponent<Text>().text = i.ToString();
                    newText.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * (float)cellHeight, 0);
                }
            }
        }
    }

    public void moveToPreviousPage()
    {
        if(actualPage == -1)
        {
            actualPage = numeroDeSegmentos; //En caso de haber p�gina parcial la anterior p�gina ser� igual al n�mero de segmentos
        }
        else
        {
            actualPage--; //En cualquier otro caso se le resta uno al n�mero de p�gina actual
        }

        foreach (UI_LineRenderer line in lines)
        {
            //Se limpian y ocultan las UI_LineRenderer para a�adir los nuevos valores
            line.points.Clear();
            line.gameObject.SetActive(false);
        }

        for (int i = 0; i < 7; i++)
        {
            int index;

            //Si no es la primera p�gina...
            if (actualPage != 0)
                index = i + (7 * (actualPage - 1));//Se calcula cual es el �ndice correspondiente en la lista completa de puntos de la gr�fica
            else
            {
                index = 0 + i;
            }

            //Se coge el valor indicado del vector completo de puntos y se agrega a la LineRenderer
            Vector2 reinforcementPoint = new Vector2(i, firstListOfPoints[index].y);
            Vector2 endurancePoint = new Vector2(i, endurancePoints[index].y);

            lines[0].points.Add(reinforcementPoint);
            lines[1].points.Add(endurancePoint);

            //Si es la primera p�gina el usuario no podr� seguir dando hacia atr�s
            if (actualPage == 1)
            {
                previousPage_Button.interactable = false;
            }

            //Si el boton de "Siguiente p�gina" estaba desactivado ahora deber� de estar abierto
            if(nextPage_Button.interactable == false)
            {
                nextPage_Button.interactable = true;
            }
            foreach (UI_LineRenderer line in lines)
            {
                line.gameObject.SetActive(true); //Se activa de nuevo finalmente ambas lineas para que muestren los nuevos valores
            }
        }

        //Se actualiza el texto de la UI
        if (endingGraphAvailable == true)
        {
            actualizarTextoDeInterfaz(actualPage, numeroDeSegmentos+1);
        }else
        {
            actualizarTextoDeInterfaz(actualPage, numeroDeSegmentos);
        }
    }

    public void moveToNextPage()
    {
        //Suma una p�gina a la p�gina actual y comprueba si est� en la �ltima p�gina
        actualPage++;

        if(actualPage > numeroDeSegmentos)
        {
            actualPage = -1;
        }

        //Se limia el LineRenderer
        foreach (UI_LineRenderer line in lines)
        {
            line.points.Clear();
            line.gameObject.SetActive(false);
        }

        //Si nos encontramos en una p�gina que no es la parcial...
        if(actualPage != -1)
        {
            for (int i = 0; i < 7; i++)
            {
                int index;

                if (actualPage >= 0)
                    index = i + (7 * (actualPage - 1));//Se calcula cual es el �ndice correspondiente en la lista completa de puntos de la gr�fica
                else
                {
                    index = 0 + i;
                }

                //Se coge el valor indicado del vector completo de puntos y se agrega a la LineRenderer
                Vector2 reinforcementPoint = new Vector2(i, firstListOfPoints[index].y);
                Vector2 endurancePoint = new Vector2(i, endurancePoints[index].y);

                lines[0].points.Add(reinforcementPoint);
                lines[1].points.Add(endurancePoint);

                //Si es la �ltima p�gina se anulara el bot�n de pasar p�gina
                if(actualPage == numeroDeSegmentos && !endingGraphAvailable)
                {
                    nextPage_Button.interactable = false;
                }
            }
        }else
        {
            for (int i = 0; i < resto; i++)
            {
                //Se coge el valor indicado del vector completo de puntos y se agrega a la LineRenderer
                Vector2 reinforcementPoint = new Vector2(i, firstListOfPoints[(firstListOfPoints.Count - resto) + i].y);
                Vector2 endurancePoint = new Vector2(i, endurancePoints[(endurancePoints.Count - resto) + i].y);

                lines[0].points.Add(reinforcementPoint);
                lines[1].points.Add(endurancePoint);

                //Es s� o s� la �ltima p�gina asi que...
                nextPage_Button.interactable = false;
            }
        }

        //Se activan de nuevo las UI_LineRenderer
        foreach (UI_LineRenderer line in lines)
        {
            line.gameObject.SetActive(true);
        }

        //Si la opci�n de volver hacia detr�s estaba desactivada entonces se activa de nuevo
        if (previousPage_Button.interactable == false)
        {
            previousPage_Button.interactable = true;
        }

        //Actualizando el texto del Canvas
        if(endingGraphAvailable)
        {
            if(actualPage == -1)
            {
                actualizarTextoDeInterfaz(numeroDeSegmentos + 1, numeroDeSegmentos + 1);
            }else
            {
                actualizarTextoDeInterfaz(actualPage, numeroDeSegmentos + 1);
            }
        }else
        {
            actualizarTextoDeInterfaz(actualPage, numeroDeSegmentos);
        }
    }

    void actualizarTextoDeInterfaz(int actualNumber, int totalNumber)
    {
        textToShow.text = actualNumber.ToString() + "/" + totalNumber.ToString();
    }

    private void creacionDeListaDeVectores()
    {
        //Rellenando la primera lista
        firstListOfPoints = SaveObject.Instance.getSpecifiedQuestionnaires(idLine_1);

        endurancePoints = SaveObject.Instance.getSpecifiedQuestionnaires(idLine_2);

        if(firstListOfPoints.Count == 0)
        {
            Debug.Log("Primera lista vacia");
            Vector2 newVector = new Vector2(0, 0);
            firstListOfPoints.Add(newVector);
        }
        if(endurancePoints.Count == 0)
        {
            Debug.Log("Segunda lista vacia");
            Vector2 newVector = new Vector2(0, 0);
            endurancePoints.Add(newVector);
        }
        //Coger los datos del save object
        //Guardarlos en la lista de puntos
        //Cambiar la lista de puntos por un diccionario
        //o por un objeto que tenga el value correspondiente y la fecha a mostrar

    }

    private void getSpecifiedQuestionnaires(string idQuest)
    {

    }
}

//Rellenando la primera lista
/*reinforcementPoints = new List<Vector2>();

reinforcementPoints.Add(new Vector2(0,(float) 5 / 4));
reinforcementPoints.Add(new Vector2(1, (float)5 / 4));
reinforcementPoints.Add(new Vector2(2, (float)4 / 4));
reinforcementPoints.Add(new Vector2(3, (float)6 / 4));
reinforcementPoints.Add(new Vector2(4, (float)4 / 4));
reinforcementPoints.Add(new Vector2(5, (float)3 / 4));
reinforcementPoints.Add(new Vector2(6, (float)2 / 4));
reinforcementPoints.Add(new Vector2(7, (float)6 / 4));
reinforcementPoints.Add(new Vector2(8, (float)1 / 4));
reinforcementPoints.Add(new Vector2(9, (float)7 / 4));
reinforcementPoints.Add(new Vector2(10, (float)1 / 4));
reinforcementPoints.Add(new Vector2(11, (float)2 / 4));
reinforcementPoints.Add(new Vector2(12, (float)6 / 4));
reinforcementPoints.Add(new Vector2(13, (float)3 / 4));
reinforcementPoints.Add(new Vector2(14, (float)8 / 4));
reinforcementPoints.Add(new Vector2(15, (float)1 / 4));
reinforcementPoints.Add(new Vector2(16, (float)3 / 4));
reinforcementPoints.Add(new Vector2(17, (float)6 / 4));
reinforcementPoints.Add(new Vector2(18, (float)2 / 4));
reinforcementPoints.Add(new Vector2(19, (float)3 / 4));
reinforcementPoints.Add(new Vector2(21, (float)3 / 4));
reinforcementPoints.Add(new Vector2(22, (float)8 / 4));
reinforcementPoints.Add(new Vector2(23, (float)8 / 4));
reinforcementPoints.Add(new Vector2(24, (float)7 / 4));
reinforcementPoints.Add(new Vector2(25, (float)3 / 4));
reinforcementPoints.Add(new Vector2(26, (float)1 / 4));
reinforcementPoints.Add(new Vector2(27, (float)8 / 4));
reinforcementPoints.Add(new Vector2(28, (float)5 / 4));
//reinforcementPoints.Add(new Vector2(29, 6));

//Rellenando la segunda lista
endurancePoints = new List<Vector2>();

endurancePoints.Add(new Vector2(0, 0));
endurancePoints.Add(new Vector2(1, (float)3 / 4));
endurancePoints.Add(new Vector2(2, (float)2 / 4));
endurancePoints.Add(new Vector2(3, (float)3 / 4));
endurancePoints.Add(new Vector2(4, (float)2 / 4));
endurancePoints.Add(new Vector2(5, (float)3 / 4));
endurancePoints.Add(new Vector2(6, (float)1 / 4));
endurancePoints.Add(new Vector2(7, (float)1 / 4));
endurancePoints.Add(new Vector2(8, (float)2 / 4));
endurancePoints.Add(new Vector2(9, (float)6 / 4));
endurancePoints.Add(new Vector2(10, (float)3 / 4));
endurancePoints.Add(new Vector2(11, (float)5 / 4));
endurancePoints.Add(new Vector2(12, (float)2 / 4));
endurancePoints.Add(new Vector2(13, (float)3 / 4));
endurancePoints.Add(new Vector2(14, (float)1 / 4));
endurancePoints.Add(new Vector2(15, (float)3 / 4));
endurancePoints.Add(new Vector2(16, (float)4 / 4));
endurancePoints.Add(new Vector2(17, (float)6 / 4));
endurancePoints.Add(new Vector2(18, (float)5 / 4));
endurancePoints.Add(new Vector2(19, (float)5 / 4));
endurancePoints.Add(new Vector2(20, (float)2 / 4));
endurancePoints.Add(new Vector2(21, (float)6 / 4));
endurancePoints.Add(new Vector2(22, (float)1 / 4));
endurancePoints.Add(new Vector2(23, (float)5 / 4));
endurancePoints.Add(new Vector2(24, (float)6 / 4));
endurancePoints.Add(new Vector2(25, (float)2 / 4));
endurancePoints.Add(new Vector2(26, (float)4 / 4));
endurancePoints.Add(new Vector2(27, (float)8 / 4));
endurancePoints.Add(new Vector2(28, (float)9 / 4));
//endurancePoints.Add(new Vector2(29, 3));*/
