using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIscript : MonoBehaviour
{
    public GameObject escMenuPanel;
    public GameObject howToPlayPanel;
    public GameObject settingsPanel;
    public GameObject mainMenuPanel;
    public GameObject quitPanel;

    public Slider sensitivitySlider;
    public ThirdPersonCam thirdPersonCam;

    public bool isPaused = false;
    public static int x;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(x);
            x++;
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
          
            escMenuPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            escMenuPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Debug.Log("selam");
        Time.timeScale = 1f;
        escMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToggleHowToPlayPanel()
    {
        escMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);

    }

    public void HowToPlayBackButton()
    {
        howToPlayPanel.SetActive(false);
        escMenuPanel.SetActive(true);

    }
    public void ToggleSettingsPanel()
    {
        escMenuPanel.SetActive(false);
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    public void SettingBackButton()
    {
        settingsPanel.SetActive(false);
        escMenuPanel.SetActive(true);

    }
    public void ToggleMainMenuPanel()
    {
        escMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
    }
    public void mainMenuNoButton()
    {
        mainMenuPanel.SetActive(false);
        escMenuPanel.SetActive(true);
    }
    public void mainMenuYesButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleQuitPanel()
    {
        escMenuPanel.SetActive(false);
        quitPanel.SetActive(true);
    }
    public void QuitMenuNoButton()
    {
        quitPanel.SetActive(false);
        escMenuPanel.SetActive(true);
    }
    public void QuitMenuYesButton()
    {
        Application.Quit();
    }

    public void OnSensitivitySliderValueChanged()
    {
        float sliderValue = sensitivitySlider.value;
        //thirdPersonCam.SetSensitivity(sliderValue);
    }
}
