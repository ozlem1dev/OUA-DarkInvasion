using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlaySound);
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
    
}
