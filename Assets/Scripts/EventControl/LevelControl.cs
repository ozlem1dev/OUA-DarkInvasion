using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelControl : MonoBehaviour
{
    public int levelNumber = 1;
    public int currentLevel = 1;
    private Spawner spawner;
    public TextMeshProUGUI currentLVLTXT;

    private void Start()
    {
        spawner = GetComponent<Spawner>();
        currentLevel = levelNumber;
        currentLVLTXT.text = currentLevel.ToString();
    }
    public void LevelCheck()
    {
        if (spawner.enemyCount >= spawner.stopNumber)
        {

            spawner.cantSpawn1 = true;
            spawner.cantSpawn2 = true;
            spawner.cantSpawn3 = true;

            //spawner.enemyCount = 0;
        }
    }
    public void LevelStart()
    {
        if (spawner.Enemies.Count == 0)
        {
            if (!spawner.isDead)
            {
                currentLevel++;

            }
            else
                spawner.isDead = false;

            spawner.cantSpawn1 = false;

            if (currentLevel >= 6)
            {
                spawner.cantSpawn2 = false;
            }
            if (currentLevel >= 12)
            {
                spawner.cantSpawn3 = false;
            }
            Debug.Log(currentLevel + ". level baþladý");
            currentLVLTXT.text = currentLevel.ToString();
            spawner.isLose = false;
        }
    }
}