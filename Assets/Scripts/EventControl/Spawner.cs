using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoint;
    public float spawnCooldown = 2f;
    public Transform[] waypoints;
    public Transform player;
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> ActiveEnemies = new List<GameObject>();
    public int stopNumber;
    public bool cantSpawn;
    public int enemyCount;

    private bool isButtonPressed = false;


    private float nextSpawnTime;
    private LevelControl level;

    private void Start()
    {
        level = gameObject.GetComponent<LevelControl>();
        InLevelCheck();
        level.currentLevel = level.levelNumber;
    }
    private void Update()
    {
        foreach (var enemy in Enemies)//Enemies listesindeki objelere waypoint atar
        {
            if (enemy != null)
            {
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                enemyMovement.player = player;


                for (int i = 0; i < waypoints.Length; i++)
                {
                    if (enemyMovement.waypoints != null && waypoints != null)
                    {
                        enemyMovement.waypoints[i] = waypoints[i];
                    }
                }
            }
        }

        if (Time.time >= nextSpawnTime && !cantSpawn)//spawn
        {

            SpawnEnemy();
            nextSpawnTime = Time.time + spawnCooldown;
            level.LevelCheck();

        }
        LevelStartApprove();

        //level.levelStartApprove();//level bittikten sonra diðer leveli baþlatmaya yarar
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, ActiveEnemies.Count);
        int randomSpawn = Random.Range(0, enemyPrefabs.Length);

        GameObject enemy = Instantiate(ActiveEnemies[randomIndex], spawnPoint[randomSpawn].position, Quaternion.identity);
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

}