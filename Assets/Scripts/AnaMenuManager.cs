using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenuManager : MonoBehaviour
{
    public MouseSensivityData sensitivityData;

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
    public Slider SoundSensitivitySlider;

    public ThirdPersonCam thirdPersonCam;

    //public SensitivityData sensitivityData;
    float mousesliderValue;
    float soundSliderValue;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LoadSensitivity();
        LoadSoundSensitivity();
        oynaButton.onClick.AddListener(() =>
        {
            oyna();
        });

        kapatButton.onClick.AddListener(() =>
         {
             kapat();
         });

        ayarlarButton.onClick.AddListener(() =>
         {
             ayarlar();
         });
        infoButton.onClick.AddListener(() =>
        {
            info();
        });
        yapimcilarButton.onClick.AddListener(() =>
        {
            yapimcilar();
        });
        ayarlarGeriButton.onClick.AddListener(() =>
        {
            geri();
        });
        infoGeriButton.onClick.AddListener(() =>
        {
            geri();
        });
        yapimcilarGeriButton.onClick.AddListener(() =>
        {
            geri();
        });

        MouseSensitivitySlider.onValueChanged.AddListener(OnSliderValueChanged);
        SoundSensitivitySlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
    }

    // Update is called once per frame
    void Update()
    {

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
    private void OnSoundSliderValueChanged(float value)
    {
        soundSliderValue = value;
        //ses seviyesi degistirme metodu buraya gelcek
        SaveSoundSensitivity();
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
        Debug.Log(sensitivityData.sensitivityValue);
        MouseSensitivitySlider.value = mousesliderValue;
        //thirdPersonCam.SetSensitivity(mousesliderValue);
    }

    void SaveSoundSensitivity()
    {
        sensitivityData.soundSensitivityValue = soundSliderValue;
    }

    void LoadSoundSensitivity()
    {
        soundSliderValue = sensitivityData.soundSensitivityValue;
        SoundSensitivitySlider.value = soundSliderValue;
        //ses seviyesi degistirme metodu buraya gelcek
    }
}

