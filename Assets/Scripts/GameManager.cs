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
    public GameObject tutorialPanel;
    public GameObject cinematicPanel1;
    public GameObject cinematicPanel2;
    public GameObject cinematicPanel3;
    public GameObject cinematicPanel4;

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
    public Button tutorialBackButton;
    public Button cinematicNextButton1;
    public Button cinematicNextButton2;
    public Button cinematicNextButton3;
    public Button cinematicNextButton4;

    public Button exitButton;
    public Button resumeButton;

    public Slider mouseSensitivitySlider;
    public Slider soundSensitivitySlider;
    public MouseSensivityData mouseSensivityData;
    public ThirdPersonCam thirdPersonCam;
    float mouseSliderValue;
    float soundSliderValue;
    private bool gamePaused = false;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        LoadSensitivity();
        LoadSoundSensitivity();
        ToggleCinematicPanel1();
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
        tutorialBackButton.onClick.AddListener(() =>
        {
            TutorialBackButton();
        });
        cinematicNextButton1.onClick.AddListener(() =>
        {
            CinematicPanelNextButton1();
        });
        cinematicNextButton2.onClick.AddListener(() =>
        {
            CinematicPanelNextButton2();
        });
        cinematicNextButton3.onClick.AddListener(() =>
        {
            CinematicPanelNextButton3();
        });
        cinematicNextButton4.onClick.AddListener(() =>
        {
            CinematicPanelNextButton4();
        });

        mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSliderValueChanged);
        soundSensitivitySlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !diePanel.activeSelf && !howToPlayPanel.activeSelf && !settingsPanel.activeSelf && !quitPanel.activeSelf && !mainMenuPanel.activeSelf)
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

    public void ToggleTutorialPanel()
    {
        tutorialPanel.SetActive(true);
        
    }

    public void TutorialBackButton()
    {
        tutorialPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
    public void QuitMenuYesButton()
    {
        Application.Quit();
    }

    public void ToggleCinematicPanel1()
    {
        Time.timeScale = 0f;
        cinematicPanel1.SetActive(true);

    }

    public void CinematicPanelNextButton1()
    {
        cinematicPanel1.SetActive(false);
        cinematicPanel2.SetActive(true);
    }
    public void ToggleCinematicPanel2()
    {
        
    }

    public void CinematicPanelNextButton2()
    {
        cinematicPanel2.SetActive(false);
        cinematicPanel3.SetActive(true);
    }

    public void ToggleCinematicPanel3()
    {
        
    }

    public void CinematicPanelNextButton3()
    {
        cinematicPanel3.SetActive(false);
        cinematicPanel4.SetActive(true);
    }

    public void ToggleCinematicPanel4()
    {
        
    }

    public void CinematicPanelNextButton4()
    {
        cinematicPanel4.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    //public void OnSensitivitySliderValueChanged()
    //{
    //    float sliderValue = sensitivitySlider.value;
    //    thirdPersonCam.SetSensitivity(sliderValue);
    //}
    private void OnSoundSliderValueChanged(float value)
    {
        soundSliderValue = value;
        //ses seviyesi degistirme metodu buraya gelcek
        SaveSoundSensitivity();
    }
    private void OnMouseSliderValueChanged(float value)
    {
        // Slider de�eri de�i�ti�inde �a�r�lacak metod
        //Debug.Log("Slider de�eri: " + value);
        mouseSliderValue = value;
        thirdPersonCam.SetSensitivity(mouseSliderValue);
        SaveSensitivity();
    }

    void SaveSensitivity()
    {
        mouseSensivityData.sensitivityValue = mouseSliderValue;
    }

    void LoadSensitivity()
    {
        mouseSliderValue = mouseSensivityData.sensitivityValue;
        mouseSensitivitySlider.value = mouseSliderValue;
        thirdPersonCam.SetSensitivity(mouseSliderValue);
    }


    void SaveSoundSensitivity()
    {
        mouseSensivityData.soundSensitivityValue = soundSliderValue;
    }

    void LoadSoundSensitivity()
    {
        soundSliderValue = mouseSensivityData.soundSensitivityValue;
        soundSensitivitySlider.value = soundSliderValue;
        //ses seviyesi degistirme metodu buraya gelcek
    }
}