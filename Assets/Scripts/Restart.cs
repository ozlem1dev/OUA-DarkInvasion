using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    Button button;
    public GameObject eventSystem;
    LevelControl levelControl;
    Spawner spawner;
    public GameObject soldier;
    CharacterHealth characterHealth;
    public GameObject base0;
    BaseHealth baseHealth;
    public  GameObject characterPanel;
    public GameObject menuPanel;
    void Start()
    {

        button=gameObject.GetComponent<Button>();
        spawner=eventSystem.GetComponent<Spawner>();
        characterHealth=soldier.GetComponent<CharacterHealth>();
        baseHealth=base0.GetComponent<BaseHealth>();
        button.onClick.AddListener(() =>
        {
            foreach(GameObject enemy in spawner.Enemies) {
                Destroy(enemy);
            }
            characterHealth.currentHealth = 100;
            characterHealth.UpdateHealthBar();
            baseHealth.currentHealth = 100;
            baseHealth.UpdateHealthBar();
            spawner.control = true;
            menuPanel.SetActive(false);
            characterPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            spawner.isDead = true;
            soldier.SetActive(false);
            soldier.SetActive(true);
            
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
