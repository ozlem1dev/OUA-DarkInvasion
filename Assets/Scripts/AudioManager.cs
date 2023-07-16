using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] MouseSensivityData mouseSensivityData;
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] AudioSource music;
    //[SerializeField] AudioSource sound;
    //public AudioMixer soundMixer;

    public Toggle musicToggle;
    public Slider musicVolumeSlider;
    public Slider soundSensitivitySlider;

    float musicSliderValue;
    float soundSliderValue;


    private void Start()
    {
        LoadIsMusic();
        LoadMusicSensitivity();
        LoadSoundSensitivity();

        musicToggle.onValueChanged.AddListener(ToggleGameMusic);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        soundSensitivitySlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
    }

    public void ToggleGameMusic(bool isOn)
    {
        if (isOn)
        {
            //music.mute = false;
            music.Play();
            mouseSensivityData.isMusicOpen = true;
        }
        else
        {
            //music.mute = true;
            music.Stop();
            mouseSensivityData.isMusicOpen = false;
        }
    }

    private void OnMusicSliderValueChanged(float value)
    {
        musicSliderValue = value;
        music.volume = musicSliderValue;
        SaveMusicSensitivity();
    }

    void SaveMusicSensitivity()
    {
        mouseSensivityData.musicSoundValue = musicSliderValue;
    }

    void LoadMusicSensitivity()
    {
        musicSliderValue = mouseSensivityData.musicSoundValue;
        musicVolumeSlider.value = musicSliderValue;
        music.volume = musicSliderValue;
    }





    private void OnSoundSliderValueChanged(float value)
    {
        soundSliderValue = value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(soundSliderValue) * 20);
        //sound.volume = soundSliderValue;
        SaveSoundSensitivity();
    }

    void SaveSoundSensitivity()
    {
        mouseSensivityData.soundSensitivityValue = soundSliderValue;
    }

    void LoadSoundSensitivity()
    {
        soundSliderValue = mouseSensivityData.soundSensitivityValue;
        soundSensitivitySlider.value = soundSliderValue;
        //sound.volume = soundSliderValue;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(soundSliderValue) * 20);
    }



    void LoadIsMusic()
    {
        musicToggle.isOn = mouseSensivityData.isMusicOpen;
        ToggleGameMusic(musicToggle.isOn);
    }
}
