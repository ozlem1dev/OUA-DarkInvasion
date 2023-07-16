using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    Button button;
    public GameObject eventSystem;
    Spawner spawner;
    public GameObject soldier;
    public GameObject base0;
    public GameObject characterPanel;
    public GameObject menuPanel;
    public GameObject gPanel;
    public GameObject characterSkill;
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        spawner = eventSystem.GetComponent<Spawner>();
        button.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            gPanel.SetActive(true);
            foreach (GameObject enemy in spawner.Enemies)
            {
                Destroy(enemy);
            }
            GameObject boss = GameObject.FindGameObjectWithTag("Enemy");
            Destroy(boss);
            Resett();
            spawner.isLose = true;
            menuPanel.SetActive(false);
            characterPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            spawner.isDead = true;

            soldier.SetActive(false);
            soldier.SetActive(true);
        });
    }


    public void Resett()
    {
        //soldier.SetActive(false);
        //soldier.SetActive(true);
        soldier.GetComponent<CharacterHealth>().ResetHealth();
        soldier.GetComponent<CharacterFire>().ResetAmmo();
        soldier.GetComponent<CharacterMana>().ResetMana();
        characterSkill.GetComponent<CharacterSkill>().isManaRefilling = false;
        characterSkill.GetComponent<CharacterSkill>().canUseSkill = true;

        base0.GetComponent<BaseHealth>().ResetHealth();

    }
}
