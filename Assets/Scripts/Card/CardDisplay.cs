using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CardDisplay : MonoBehaviour
{
    public TextMeshProUGUI rightOptionText;
    public TextMeshProUGUI leftOptionText;
    public GameObject model; //Prefab del personaje
    public GameObject sceneModel; //el gameObject que hay en la escena para colocar la malla
    public bool startedInitialMovement = false;
    bool changingColor = false;

    public Card currentCard;
    private float rotationSpeed = 150f;


    protected virtual void Start()
    {
        currentCard = GameManager.Instance.currentCard;  
        DisplayCard();
    }

    public virtual void DisplayCard()
    {
        
    }

    public virtual void ShowOption(Single.Direction direction)
    {
        
        switch (direction)
        {
            case Single.Direction.RIGHT:
                StartCoroutine(chosingRightOption(0.5f));

                break;
            case Single.Direction.LEFT:
                StartCoroutine(chosingLeftOption(0.5f));

                break;
            case Single.Direction.NONE:
                StartCoroutine(chosingNoneOption(0.5f));

                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (startedInitialMovement)
        {
            if (transform.rotation.y > 0)
            {
                transform.RotateAround(transform.position, -transform.up, Time.deltaTime * rotationSpeed);
            }
            else
            {
                Camera.main.orthographic = true;
                startedInitialMovement = false;
            }
        }
    }

    public IEnumerator chosingRightOption(float duration)
    {
        if (changingColor)
        {
            yield break;
        }

        rightOptionText.fontStyle = FontStyles.Bold;
        leftOptionText.fontStyle = FontStyles.Normal;

        float timer = 0;
        Color rightTextColor = rightOptionText.color;
        Color leftTextColor = leftOptionText.color;

        Color activatedTextColor = Color.green;
        Color desactivatedTextColor = Color.red;
        desactivatedTextColor.a = 0.25f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float actualTime = timer / duration;

            rightOptionText.color = Color.Lerp(rightTextColor, activatedTextColor, actualTime);
            leftOptionText.color = Color.Lerp(leftTextColor, desactivatedTextColor, actualTime);

            yield return null;
        }

        changingColor = false;
    }

    public IEnumerator chosingLeftOption(float duration)
    {
        if(changingColor)
        {
            yield break;
        }

        leftOptionText.fontStyle = FontStyles.Bold;
        rightOptionText.fontStyle = FontStyles.Normal;

        float timer = 0;
        Color rightTextColor = rightOptionText.color;
        Color leftTextColor = leftOptionText.color;

        Color activatedTextColor = Color.green;
        Color desactivatedTextColor = Color.red;
        desactivatedTextColor.a = 0.25f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float actualTime = timer / duration;

            leftOptionText.color = Color.Lerp(leftTextColor, activatedTextColor, actualTime);
            rightOptionText.color = Color.Lerp(rightTextColor, desactivatedTextColor, actualTime);

            yield return null;
        }

        changingColor = false;
    }


    public IEnumerator chosingNoneOption(float duration)
    {
        if (changingColor)
        {
            yield break;
        }

        rightOptionText.fontStyle = FontStyles.Normal;
        leftOptionText.fontStyle = FontStyles.Normal;

        float timer = 0;
        Color rightTextColor = rightOptionText.color;
        Color leftTextColor = leftOptionText.color;

        Color activatedTextColor = Color.white;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float actualTime = timer / duration;

            rightOptionText.color = Color.Lerp(rightTextColor, activatedTextColor, actualTime);
            leftOptionText.color = Color.Lerp(leftTextColor, activatedTextColor, actualTime);

            yield return null;
        }

        changingColor = false;
    }
}

public interface IDisplay
{
    void DisplayCard();
}
