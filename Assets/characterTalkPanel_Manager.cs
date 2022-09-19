using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class characterTalkPanel_Manager : MonoBehaviour
{
    public RawImage characterRawImage;
    public Text characterName;

    //Normal talk Content
    public GameObject normalTalkPanel_GameObject;
    public Text conversationText;


    //Choice Talk Content
    public GameObject choicePanel_GameObject;
    public Text askText;


    public GameObject talkingCitizen;

    public void setUpPanel(GameObject citizen)
    {
        talkingCitizen = citizen;
        talkingCitizen.GetComponent<SimpleAgent>().enabled = false;
        talkingCitizen.transform.GetChild(1).gameObject.SetActive(true);
        talkingCitizen.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Citizen");

        //Set name
        characterName.text = talkingCitizen.transform.GetChild(0).gameObject.GetComponent<Local>().localName;

        if (talkingCitizen.transform.GetChild(0).gameObject.GetComponent<LocalsMessages>().specialCitizenWithConflict)
        {
            choicePanel_GameObject.SetActive(true);
            askText.text = talkingCitizen.transform.GetChild(0).gameObject.GetComponent<LocalsMessages>().getMissionRandomMessage();
        }
        else
        {
            normalTalkPanel_GameObject.SetActive(true);
            conversationText.text = talkingCitizen.transform.GetChild(0).gameObject.GetComponent<LocalsMessages>().getRandomMessage();
        }
    }

    public void endConversation()
    {
        CanvasController mainCanvas = (CanvasController)FindObjectOfType(typeof(CanvasController));
        mainCanvas.showingConversation = false;

        talkingCitizen.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        talkingCitizen.transform.GetChild(1).gameObject.SetActive(false);
        talkingCitizen.GetComponent<SimpleAgent>().enabled = true;

        Destroy(this.gameObject);
    }

    public void startDecisionTaking()
    {
        GameManager.Instance.SaveGame();
        SceneManager.LoadScene("DecisionMakingGame");
    }
}
