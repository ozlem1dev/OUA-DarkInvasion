using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePrefabScript : MonoBehaviour
{
    public float time = 3f;
    public int startGrenadeDamage;
    public static int currentGrenadeDamage = 20;
    public GameObject grenadeExplosionAudioPrefab;
    public GameObject explosionPrefab; // Patlama efekti prefab

    private void Start()
    {
        StartCoroutine(ExplodeAfterTime(time));
    }

    private IEnumerator ExplodeAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 100f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyHealth>().takeDamage(currentGrenadeDamage);
            }
        }

        // Patlama efekti prefabini etkinlestir
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject audioExplosion = Instantiate(grenadeExplosionAudioPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 3f); // Belirli bir süre sonra patlama efektini yok et
        Destroy(audioExplosion, 3f);
        Destroy(gameObject);
    }
    public void UpdateGrenadeDamage(int newDamage)
    {
        currentGrenadeDamage = (100 + newDamage) * currentGrenadeDamage / 100;
    }
}