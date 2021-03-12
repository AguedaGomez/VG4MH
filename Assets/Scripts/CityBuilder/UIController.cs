using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider activationSlider;
    public Text materials;
    public Button powerRButton;
    public City city;
    public InputField stepsInputField; //change to text?

    private Image powerRButtonImg;


    // Start is called before the first frame update
    void Start()
    {
        powerRButtonImg = powerRButton.GetComponent<Image>();
    }

    void Update()
    {
        ShowActivation();
        ShowMaterials();
        ShowPowerR();
    }

    private void ShowActivation()
    {
        activationSlider.value = city.Activation;
    }
    private void ShowMaterials()
    {
        materials.text = "Materiales: " + city.Materials;
    }

    //TODO REPLACE THIS METHOD IN UPDATE
    private void ShowPowerR()
    {
        powerRButtonImg.color = new Color(city.powerR / 100f, 0.525f, 0.645f, 1);
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
