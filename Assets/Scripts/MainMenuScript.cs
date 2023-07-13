using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject howToPlayPanel;
    public GameObject creditsPanel;
    public GameObject settingsPanel;


    public Slider sensitivitySlider;
    public ThirdPersonCam thirdPersonCam;

    void Start()
    {

    }


    void Update()
    {

    }

    public void ToggleHowToPlayPanel()
    {
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);

    }

    public void HowToPlayBackButton()
    {
        mainPanel.SetActive(true);
        howToPlayPanel.SetActive(false);

    }
    public void ToggleCreditsPanel()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(!creditsPanel.activeSelf);

    }

    public void CreditsBackButton()
    {
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void ToggleSettingsPanel()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void SettingsBackButton()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);

    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    public void OnSensitivitySliderValueChanged()
    {
        float sliderValue = sensitivitySlider.value;
        thirdPersonCam.SetSensitivity(sliderValue);
    }
}
