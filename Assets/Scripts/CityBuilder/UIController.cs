using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject messagesPanel;
    public Text messagesText;
    public Slider activationSlider;
    public Text materials;
    public Button powerRButton;
    public LocalsMessages localsMessages;
    public City city;
    public InputField stepsInputField; //change to text?
    public List<Button> buildingButtons = new List<Button>();

    private Image powerRButtonImg;
    private Dictionary<string, Button> buildingButtonsDictionary = new Dictionary<string, Button>();


    // Start is called before the first frame update
    void Start()
    {
        localsMessages.ShowGeneralMessage += LocalsMessages_ShowGeneralMessage;
        powerRButtonImg = powerRButton.GetComponent<Image>();
        CreateDictionaryButtons();
    }

    private void LocalsMessages_ShowGeneralMessage(string message)
    {
        messagesText.text = message;
        messagesPanel.SetActive(true);
    }

    void Update()
    {
        ShowActivation();
        ShowMaterials();
        ShowPowerR();
        CheckBuildingButtonsAvailability();
    }

    private void CreateDictionaryButtons()
    {
        buildingButtonsDictionary = buildingButtons.ToDictionary(b=>b.name, b=>b) ;
        
    }
    private void ShowActivation()
    {
        activationSlider.value = city.Activation;
    }
    private void ShowMaterials()
    {
        materials.text = "Materiales: " + city.Materials;
    }

    private void ShowPowerR()
    {
        powerRButtonImg.color = new Color(city.powerR / 100f, 0.525f, 0.645f, 1);
    }
    private void CheckBuildingButtonsAvailability()
    {
        foreach (var b in city.availableBuildings)
        {
            buildingButtonsDictionary[b.Key].interactable = true;
        }
    }

    public void ShowAndHideMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void ChangeToogleLibraryState (Toggle toggle)
    {
        city.VisitLibrary(toggle.isOn);
    }
    public void ChangeToogleQuestionaryState(Toggle toggle)
    {
        city.FinishQuestionary(toggle.isOn);
    }

    //from GUI
    public void SaveCurrentSteps ()
    {
        city.SaveCurrentSteps(int.Parse(stepsInputField.text));
        stepsInputField.text = "";
    }


}
