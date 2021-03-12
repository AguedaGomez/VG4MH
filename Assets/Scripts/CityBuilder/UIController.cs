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
    public InputField stepsInputField;

    private Image powerRButtonImg;

    private TouchScreenKeyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        powerRButtonImg = powerRButton.GetComponent<Image>();
    }

    void Update()
    {
        if (keyboard != null)
            keyboard.active = true;
        ShowActivation();
        ShowMaterials();
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
    public void ShowPowerR()
    {
        powerRButtonImg.color = new Color(0.18f, 0.4f, 0.83f, city.powerR / 100f);
    }

    public void ShowAndHideMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }

    //from GUI
    public void SaveCurrentSteps ()
    {
        city.SaveCurrentSteps(int.Parse(stepsInputField.text));
    }
}
