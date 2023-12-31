using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public AudioSource buttonClickAudioSource;

    public Spawner spawner;
    public GameObject gPanel;
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
    public GameObject winPanel;
    public GameObject bookPanel;
    public GameObject towerPanel;

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
    public Button winPanelMainMenuButton;

    public Button exitButton;
    public Button resumeButton;

    public Slider mouseSensitivitySlider;

    public MouseSensivityData mouseSensivityData;
    public ThirdPersonCam thirdPersonCam;
    float mouseSliderValue;

    private bool gamePaused = false;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        LoadSensitivity();

        if (mouseSensivityData.firstOpen)
        {
            ToggleCinematicPanel1();
            mouseSensivityData.firstOpen = false;
        }
        else
            tutorialPanel.SetActive(true);

        resumeButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            ResumeGame();
        });

        exitButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            ToggleQuitPanel();
        });

        howToPlay.onClick.AddListener(() =>
        {
            ToggleHowToPlayPanel();
        });

        howToPlayBack.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            HowToPlayBackButton();
        });
        settings.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            ToggleSettingsPanel();
        });
        settingsBack.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            SettingBackButton();
        });
        mainMenu.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            ToggleMainMenuPanel();
        });
        mainMenuNo.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            mainMenuNoButton();
        });
        mainMenuYes.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            mainMenuYesButton();
        });
        quitYesButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            QuitMenuYesButton();
        });
        quitNoButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            QuitMenuNoButton();
        });
        dieMainMenuButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            mainMenuYesButton();
        });
        tutorialBackButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            TutorialBackButton();
        });
        cinematicNextButton1.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            CinematicPanelNextButton1();
        });
        cinematicNextButton2.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            CinematicPanelNextButton2();
        });
        cinematicNextButton3.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            CinematicPanelNextButton3();
        });
        cinematicNextButton4.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            CinematicPanelNextButton4();
        });
        winPanelMainMenuButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            WinPanelMainMenuButton();
        });

        mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSliderValueChanged);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !diePanel.activeSelf && !howToPlayPanel.activeSelf && !settingsPanel.activeSelf && !quitPanel.activeSelf && !mainMenuPanel.activeSelf && !tutorialPanel.activeSelf && !bookPanel.activeSelf && !cinematicPanel1.activeSelf && !cinematicPanel2.activeSelf && !cinematicPanel3.activeSelf && !cinematicPanel4.activeSelf && !winPanel.activeSelf && !towerPanel.activeSelf)//xxx
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

        if (spawner.Enemies.All(x => x == null) && spawner.bossObject == null)
        {
            gPanel.SetActive(true);
        }
        escMenuPanel.SetActive(false);
        gamePaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void PauseGame()
    {
        Time.timeScale = 0f;
        gPanel.SetActive(false);
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
    public void ToggleWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void WinPanelMainMenuButton()
    {
        winPanel.SetActive(false);
        SceneManager.LoadScene(0);

    }

    //public void OnSensitivitySliderValueChanged()
    //{
    //    float sliderValue = sensitivitySlider.value;
    //    thirdPersonCam.SetSensitivity(sliderValue);
    //}

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
}
