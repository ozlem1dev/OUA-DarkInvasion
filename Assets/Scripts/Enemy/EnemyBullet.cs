using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float time = 3f;
    private bool collided = false;
    public int ammoDamage = 20;

    private void Start()
    {
        Destroy(gameObject, time);
    }

    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (!collided)
    //    {
    //        collided = true;
    //        // Sadece mermiyi yok etmek için:
    //        Destroy(gameObject);
    //    }

    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        //gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //        collision.gameObject.GetComponent<CharacterHealth>().takeDamage(ammoDamage);
    //        //collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    //        Destroy(gameObject);

    //    }
    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (!collided)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collided = false;
            } 
            else
            {
                collided = true;
                Destroy(gameObject);
            }
            
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<CharacterHealth>().takeDamage(ammoDamage);
            //collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(gameObject);
        }

    }
}
