using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenuManager : MonoBehaviour
{
    public MouseSensivityData sensitivityData;

    [SerializeField] AudioSource buttonClickAudioSource;

    public GameObject mainPanel;
    public GameObject howToPlayPanel;
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    public Button oynaButton;
    public Button kapatButton;

    public Button ayarlarButton;
    public Button infoButton;
    public Button yapimcilarButton;

    public Button ayarlarGeriButton;
    public Button infoGeriButton;
    public Button yapimcilarGeriButton;

    public Slider MouseSensitivitySlider;

    public ThirdPersonCam thirdPersonCam;

    float mousesliderValue;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LoadSensitivity();
        oynaButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            oyna();
        });

        kapatButton.onClick.AddListener(() =>
         {
             buttonClickAudioSource.Play();
             kapat();
         });

        ayarlarButton.onClick.AddListener(() =>
         {
             buttonClickAudioSource.Play();
             ayarlar();
         });
        infoButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            info();
        });
        yapimcilarButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            yapimcilar();
        });
        ayarlarGeriButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            geri();
        });
        infoGeriButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            geri();
        });
        yapimcilarGeriButton.onClick.AddListener(() =>
        {
            buttonClickAudioSource.Play();
            geri();
        });

        MouseSensitivitySlider.onValueChanged.AddListener(OnSliderValueChanged);

    }

    void oyna()
    {
        SceneManager.LoadScene(1);
    }
    void kapat()
    {
        Application.Quit();
    }

    void ayarlar()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    void info()
    {
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    void yapimcilar()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    void geri()
    {
        mainPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void OnSliderValueChanged(float value)
    {
        mousesliderValue = value;
        thirdPersonCam.SetSensitivity(mousesliderValue);
        SaveSensitivity();
    }
    void SaveSensitivity()
    {
        sensitivityData.sensitivityValue = mousesliderValue;
    }

    void LoadSensitivity()
    {
        mousesliderValue = sensitivityData.sensitivityValue;
        MouseSensitivitySlider.value = mousesliderValue;
    }


}

