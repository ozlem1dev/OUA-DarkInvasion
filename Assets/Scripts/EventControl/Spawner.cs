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
    public GameObject soldier;
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> ActiveEnemies = new List<GameObject>();
    public int stopNumber;
    public bool cantSpawn1;
    public bool cantSpawn2;
    public bool cantSpawn3;
    public int enemyCount = 0;
    public bool isLose = false;
    public GameObject characterPanel;
    public GameObject bookPanel;
    public GameObject boss;
    public bool isDead = false;
    public bool control;
    private bool isButtonPressed = false;
    private float nextSpawnTime1;
    private float nextSpawnTime2;
    private float nextSpawnTime3;
    private LevelControl level;
    private EnemyHealth ehealth;

    private bool isCardSelection = true;
    private bool isBossSpawned = false;
    CharacterFire _characterfire;
    KartMek _kartmek;

    private void Start()
    {
        cantSpawn1 = false;
        cantSpawn2 = true;
        cantSpawn3 = true;

        level = gameObject.GetComponent<LevelControl>();
        _kartmek = bookPanel.GetComponent<KartMek>();
        _characterfire = soldier.GetComponent<CharacterFire>();

        InLevelCheck();
        level.currentLevel = level.levelNumber;
    }
    private void Update()
    {

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
        if (level.currentLevel == 10 && !isBossSpawned)
        {

            GameObject bossObject=Instantiate(boss, spawnPoint1[randomSpawn].position, Quaternion.identity);
            BossMovement bossMovement = bossObject.GetComponent<BossMovement>();

           
            
                for (int i = 0; i < waypoints1.Length; i++)
                {
                    if (bossMovement.waypoints != null && waypoints1 != null)
                    {
                        bossMovement.waypoints[i] = waypoints1[i];
                    }
                }
                
            enemyCount++;
            isBossSpawned = true;
        }
        GameObject enemy = Instantiate(ActiveEnemies[randomIndex], spawnPoint1[randomSpawn].position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().spawned1 = true;
        Enemies.Add(enemy);
        enemyCount++;
    }
    private void SpawnEnemy2()
    {

        int randomIndex = Random.Range(0, ActiveEnemies.Count);
        int randomSpawn = Random.Range(0, spawnPoint2.Length);
        if (level.currentLevel == 15 && !isBossSpawned)
        {
            Instantiate(boss, spawnPoint1[randomSpawn].position, Quaternion.identity);
            GameObject bossObject = Instantiate(boss, spawnPoint1[randomSpawn].position, Quaternion.identity);
            BossMovement bossMovement = bossObject.GetComponent<BossMovement>();



            for (int i = 0; i < waypoints1.Length; i++)
            {
                if (bossMovement.waypoints != null && waypoints2 != null)
                {
                    bossMovement.waypoints[i] = waypoints2[i];
                }
            }
            Enemies.Add(bossObject);
            enemyCount++;
            isBossSpawned = true;
        }
        GameObject enemy = Instantiate(ActiveEnemies[randomIndex], spawnPoint2[randomSpawn].position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().spawned2 = true;
        Enemies.Add(enemy);
        enemyCount++;
    }
    private void SpawnEnemy3()
    {

        int randomIndex = Random.Range(0, ActiveEnemies.Count);
        int randomSpawn = Random.Range(0, spawnPoint3.Length);
        if (level.currentLevel == 20 && !isBossSpawned)
        {
            Instantiate(boss, spawnPoint1[randomSpawn].position, Quaternion.identity);
            GameObject bossObject = Instantiate(boss, spawnPoint1[randomSpawn].position, Quaternion.identity);
            BossMovement bossMovement = bossObject.GetComponent<BossMovement>();

            for (int i = 0; i < waypoints1.Length; i++)
            {
                if (bossMovement.waypoints != null && waypoints3 != null)
                {
                    bossMovement.waypoints[i] = waypoints3[i];
                }
            }
            Enemies.Add(bossObject);
            enemyCount++;
            isBossSpawned = true;
        }
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


                ActiveEnemies.Add(enemy);
            }
        }
    }


    public void LevelStartApprove()
    {
        if (Enemies.All(x => x == null) && isCardSelection && !isLose)
        {
            isCardSelection = false;
            control = true;
            StartCoroutine(DelayedDisplayCards());
        }

        // B tuþu ile kitabý açýp kapatabilmek için.
        //    if (Input.GetKeyDown(KeyCode.B )&& !bookPanel.activeSelf) 
        //{
        //    if(Enemies.All(x => x == null))
        //    {
        //        _characterfire.clipSize = 30;
        //        _kartmek.DisplayCards();

        //        characterPanel.SetActive(false);
        //        bookPanel.SetActive(true);
        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.visible = true;
        //    }

        //}
        //else if (Input.GetKeyDown(KeyCode.B) && bookPanel.activeSelf)
        //{
        //    characterPanel.SetActive(true);
        //    bookPanel.SetActive(false);
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}

        if (Input.GetKeyDown(KeyCode.G) && !isButtonPressed && !control)
        {
            _characterfire.ResetAmmo();
            stopNumber += 5;
            isBossSpawned = false;
            
            if (Enemies.All(obj => obj == null))
            {
                isButtonPressed = true;
                Enemies.RemoveAll(obj => obj == null || obj.Equals(null));
                enemyCount = 0;

                StartCoroutine(DelayedLevelStart());
            }
        }
    }

    void DisplayCardsOnScreen()
    {
        characterPanel.SetActive(false);
        bookPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _kartmek.DisplayCards();
    }

    IEnumerator DelayedDisplayCards()
    {
        yield return new WaitForSeconds(3f);
        DisplayCardsOnScreen();

    }


    IEnumerator DelayedLevelStart()
    {
        yield return new WaitForSeconds(3f);
        level.LevelStart();
        InLevelCheck();
        isButtonPressed = false; // Butonun tekrar basýlabileceðini iþaretlemek için false yapýlýyor

        isCardSelection = true;
    }

    private void WaypointGiver1()
    {
        foreach (var enemy in Enemies)//Enemies listesindeki objelere waypoint atar
        {
            if (enemy != null)
            {
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                enemyMovement.player = soldier.transform;

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