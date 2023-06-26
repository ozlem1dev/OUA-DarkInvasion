using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelControl : MonoBehaviour
{
    public int levelNumber = 1;
    public int currentLevel = 1;
    private Spawner spawner;
   

    private void Start()
    {
        spawner = GetComponent<Spawner>();
        currentLevel = levelNumber;
    }
    public void LevelCheck()
    {
        if (spawner.enemyCount == spawner.stopNumber)
        {

            spawner.cantSpawn = true;

            spawner.enemyCount = 0;
        }
    }
    public void LevelStart()
    {
        if (spawner.Enemies.Count == 0)
        {
            currentLevel++;
            spawner.cantSpawn = false;
            Debug.Log(currentLevel + ". level baþladý");
        }
    }
}