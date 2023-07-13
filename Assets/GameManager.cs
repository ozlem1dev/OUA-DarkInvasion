using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject escMenuPanel;
    
    public GameObject howToPlayPanel;
    public GameObject settingsPanel;
    public GameObject mainMenuPanel;
    public GameObject quitPanel;
    public GameObject diePanel;

    public Button howToPlay;
    public Button howToPlayBack;
    public Button settings;
    public Button settingsBack;
    public Button mainMenu;
    public Button mainMenuYes;
    public Button mainMenuNo;
    public Button quitYesButton;
    public Button quitNoButton;
    public Button dieMainMenuButton;

    public Button exitButton;
    public Button resumeButton;

    public Slider sensitivitySlider;
    public ThirdPersonCam thirdPersonCam;

    private bool gamePaused = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        resumeButton.onClick.AddListener(() =>
        {
            ResumeGame();
        });

        exitButton.onClick.AddListener(() =>
        {
            ToggleQuitPanel();
        });

        howToPlay.onClick.AddListener(() =>
        {
            ToggleHowToPlayPanel();
        });

        howToPlayBack.onClick.AddListener(() =>
        {
            HowToPlayBackButton();
        });
        settings.onClick.AddListener(() =>
        {
            ToggleSettingsPanel();
        });
        settingsBack.onClick.AddListener(() =>
        {
            SettingBackButton();
        });
        mainMenu.onClick.AddListener(() =>
        {
            ToggleMainMenuPanel();
        });
        mainMenuNo.onClick.AddListener(() =>
        {
            mainMenuNoButton();
        });
        mainMenuYes.onClick.AddListener(() =>
        {
            mainMenuYesButton();
        });
        quitYesButton.onClick.AddListener(() =>
        {
            QuitMenuYesButton();
        });
        quitNoButton.onClick.AddListener(() =>
        {
            QuitMenuNoButton();
        });
        dieMainMenuButton.onClick.AddListener(() =>
        {
            mainMenuYesButton();
        });

        sensitivitySlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && diePanel.activeSelf == false)
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void ResumeGame()
    {
        Time.timeScale = 1f;

        escMenuPanel.SetActive(false);
        gamePaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void PauseGame()
    {
        Time.timeScale = 0f;

        escMenuPanel.SetActive(true);
        gamePaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        Cursor.visible = true;
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

        Time.timeScale = 1f;

        escMenuPanel.SetActive(false);
        gamePaused = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    //public void OnSensitivitySliderValueChanged()
    //{
    //    float sliderValue = sensitivitySlider.value;
    //    thirdPersonCam.SetSensitivity(sliderValue);
    //}

    private void OnSliderValueChanged(float value)
    {
        // Slider deðeri deðiþtiðinde çaðrýlacak metod
        //Debug.Log("Slider deðeri: " + value);
        float sliderValue = value;
        thirdPersonCam.SetSensitivity(sliderValue);
    }
}