using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public Transform[] spawnPoint; 
    public float spawnCooldown = 2f; 
    public Transform[] waypoints;
    public Transform player;
    public List<GameObject> Enemies = new List<GameObject>();
    private float nextSpawnTime;

    private void Start()
    {

        
    }
    private void Update()
    {
        foreach (var enemy in Enemies)
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
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnCooldown; 
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        
        GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPoint[randomIndex].position, Quaternion.identity);
        Enemies.Add(enemy);
        
    }

    
}
