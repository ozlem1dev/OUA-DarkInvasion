using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Transform player;
    private void Update()
    {
       

        Vector3 dir = player.position - transform.position;
        float distanceThisFrame = 10f * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        var playerHealth = player.GetComponent<CharacterHealth>();
        playerHealth.takeDamage(10);
        Destroy(gameObject);
    }
}
