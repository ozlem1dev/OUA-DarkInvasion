using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSpawnZone : MonoBehaviour
{
    GameObject eventSystem;
    TowerPoints towerPoints;
    public Material glowMaterial;
    private Material originalMaterial;
    private Renderer objectRenderer;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        //kartMek = GetComponent<KartMek>();
        eventSystem = GameObject.Find("Event System");
        towerPoints = eventSystem.GetComponent<TowerPoints>();
    }

    public void CreateTower()
    {
        var towerPrefab = KartMek.towerPrefab;
        var target = gameObject.GetComponentInChildren<Transform>();

        // Kule prefabýný spawn noktasýnýn transformuna eþitleyerek kuleyi spawn noktasýna taþý
        Instantiate(towerPrefab, gameObject.transform.position, gameObject.transform.rotation);


        towerPoints.isDone = false;
        towerPoints.isSelectCreatedTowerCard = false;
        eventSystem.GetComponent<Spawner>().control = false;
    }


    private Bounds CalculateBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }


    void OnMouseDown()
    {
        if (towerPoints.isSelectCreatedTowerCard)
        {
            var pointSpawnTower = towerPoints.Points.FirstOrDefault(x => x == towerPoints.Points[TowerPoints.currentIndex]);
            if (pointSpawnTower != null && pointSpawnTower.transform.position == gameObject.transform.position)
            {
                CreateTower();
                towerPoints.FollowCharacter();

                if (towerPoints.Points.Count != 0)
                    StartCoroutine(DelayRemovePointSpawn(pointSpawnTower));
            }
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("geldi");
        objectRenderer.material = glowMaterial;
    }
    private void OnMouseExit()
    {
        Debug.Log("gitti");
        objectRenderer.material = originalMaterial;
    }

    IEnumerator DelayRemovePointSpawn(GameObject pointSpawnTower)
    {
        yield return new WaitForSeconds(3f);
        RemovePointSpawn(pointSpawnTower);
    }
    void RemovePointSpawn(GameObject pointSpawnTower)
    {
        towerPoints.Points.Remove(pointSpawnTower);
        StartCoroutine(DelayDestroytPointSpawn(pointSpawnTower));
    }
    IEnumerator DelayDestroytPointSpawn(GameObject pointSpawnTower)
    {
        yield return new WaitForSeconds(2f);
        Destroy(pointSpawnTower);
    }
}