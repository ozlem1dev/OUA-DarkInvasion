using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePrefabScript : MonoBehaviour
{
    public float time = 3f;
    public int grenadeDamage = 20;

    private void Start()
    {
        StartCoroutine(ExplodeAfterTime(time));
    }
    

    private IEnumerator ExplodeAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f); // El bombasýnýn etkileþim alanýndaki nesneleri al

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyHealth>().takeDamage(grenadeDamage);
            }
            //var s= gameObject.GetComponentInChildren<GrenadePrefabScript>();
            //s.gameObject.
        }

        Destroy(gameObject);
    }
}