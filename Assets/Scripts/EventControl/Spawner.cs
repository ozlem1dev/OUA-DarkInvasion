using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoint1;
    public Transform[] spawnPoint2;
    public Transform[] spawnPoint3;
    public float spawnCooldown = 2f;
    public Transform[] waypoints1;
    public Transform[] waypoints2;
    public Transform[] waypoints3;
    public Transform player;
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> ActiveEnemies = new List<GameObject>();
    public int stopNumber;
    public bool cantSpawn1;
    public bool cantSpawn2=true;
    public bool cantSpawn3=true;
    public int enemyCount;
    

    private bool isButtonPressed = false;


    private float nextSpawnTime1;
    private float nextSpawnTime2;
    private float nextSpawnTime3;
    private LevelControl level;

    private void Start()
    {
        cantSpawn1 = false;
        cantSpawn2 = true;
        cantSpawn3 = true;
        level = gameObject.GetComponent<LevelControl>();
        InLevelCheck();
        level.currentLevel = level.levelNumber;
    }
    private void Update()
    {
        if(level.currentLevel>=2) 
        {
            cantSpawn2 = false;
        }
        if (level.currentLevel >= 3)
        {
            cantSpawn3 = false;
        }
        WaypointGiver1();
        if (Time.time >= nextSpawnTime1 && !cantSpawn1)//spawn
        {

            SpawnEnemy1();
            nextSpawnTime1 = Time.time + spawnCooldown;
            level.LevelCheck();

        }
        if (Time.time >= nextSpawnTime2 && !cantSpawn2)//spawn
        {
           
            SpawnEnemy2();
            nextSpawnTime2 = Time.time + spawnCooldown;
            level.LevelCheck();

        }
        if (Time.time >= nextSpawnTime3 && !cantSpawn3)//spawn
        {

            SpawnEnemy3();
            nextSpawnTime3 = Time.time + spawnCooldown;
            level.LevelCheck();

        }
        LevelStartApprove();

        
    }

    private void SpawnEnemy1()
    {
        int randomIndex = Random.Range(0, ActiveEnemies.Count);
        int randomSpawn = Random.Range(0, spawnPoint1.Length);

        GameObject enemy = Instantiate(ActiveEnemies[randomIndex], spawnPoint1[randomSpawn].position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().spawned1 = true;
        Enemies.Add(enemy);
        enemyCount++;
    }
    private void SpawnEnemy2()
    {
        Debug.Log("2.spawn");
        int randomIndex = Random.Range(0, ActiveEnemies.Count);
        int randomSpawn = Random.Range(0, spawnPoint2.Length);

        GameObject enemy = Instantiate(ActiveEnemies[randomIndex], spawnPoint2[randomSpawn].position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().spawned2 = true;
        Enemies.Add(enemy);
        enemyCount++;
    }
    private void SpawnEnemy3()
    {
        Debug.Log("2.spawn");
        int randomIndex = Random.Range(0, ActiveEnemies.Count);
        int randomSpawn = Random.Range(0, spawnPoint3.Length);

        GameObject enemy = Instantiate(ActiveEnemies[randomIndex], spawnPoint3[randomSpawn].position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().spawned3 = true;
        Enemies.Add(enemy);
        enemyCount++;
    }
    private void InLevelCheck()
    {
        ActiveEnemies.Clear();
        foreach (GameObject enemy in enemyPrefabs)
        {
            var x = enemy.GetComponent<EnemyMovement>();

            if (x.minLvl <= level.currentLevel && x.maxLvl >= level.currentLevel)
            {
                Debug.Log(x.name + " min " + x.minLvl + " max " + x.maxLvl + " current level: " + level.currentLevel);

                ActiveEnemies.Add(enemy);
            }
        }
    }


    public void LevelStartApprove()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isButtonPressed)
        {
            if (Enemies.All(obj => obj == null))
            {
                isButtonPressed = true;
                Enemies.RemoveAll(obj => obj == null || obj.Equals(null));
                StartCoroutine(DelayedLevelStart());
            }
        }
    }

    IEnumerator DelayedLevelStart()
    {
        yield return new WaitForSeconds(3f);
        level.LevelStart();
        InLevelCheck();
        isButtonPressed = false; // Butonun tekrar basýlabileceðini iþaretlemek için false yapýlýyor
    }

    private void WaypointGiver1()
    {
        foreach (var enemy in Enemies)//Enemies listesindeki objelere waypoint atar
        {
            if (enemy != null)
            {
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                enemyMovement.player = player;

                if (enemyMovement.spawned1 == true)
                {
                    for (int i = 0; i < waypoints1.Length; i++)
                    {
                        if (enemyMovement.waypoints != null && waypoints1 != null)
                        {
                            enemyMovement.waypoints[i] = waypoints1[i];
                        }
                    }
                }
                if (enemyMovement.spawned2 == true)
                {
                    for (int i = 0; i < waypoints2.Length; i++)
                    {
                        if (enemyMovement.waypoints != null && waypoints2 != null)
                        {
                            enemyMovement.waypoints[i] = waypoints2[i];
                        }
                    }
                }
                if (enemyMovement.spawned3 == true)
                {
                    for (int i = 0; i < waypoints3.Length; i++)
                    {
                        if (enemyMovement.waypoints != null && waypoints2 != null)
                        {
                            enemyMovement.waypoints[i] = waypoints3[i];
                        }
                    }
                }

            }
        }
    }
    
}