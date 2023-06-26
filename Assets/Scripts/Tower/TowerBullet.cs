using UnityEditor;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public float speed = 70f;
    public float damage = 10f;
    private Transform target;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        var enemyHealth = target.GetComponent<EnemyHealth>();
        enemyHealth.takeDamage(10);
        Destroy(gameObject);
    }
}
