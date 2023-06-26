using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float time = 3f;
    private bool collided = false;
    public int ammoDamage = 20;

    private void Start()
    {
        Destroy(gameObject, time);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collided)
        {
            collided = true;
            // Sadece mermiyi yok etmek için:
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
        collision.gameObject.GetComponent<EnemyHealth>().takeDamage(ammoDamage);
        }
    }
}
