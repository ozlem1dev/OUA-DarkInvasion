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
    /*public GameObject bookPanel;
    private KartMek kartMek;*/
    void Start()
    {
        //kartMek = GetComponent<KartMek>();
        eventSystem = GameObject.Find("Event System");
        towerPoints = eventSystem.GetComponent<TowerPoints>();
    }

    public void CreateTower()
    {

        var towerPrefab = KartMek.towerPrefab;
        Instantiate(towerPrefab, gameObject.transform.position, Quaternion.identity);
        towerPoints.isDone = false;
        towerPoints.isSelectCreatedTowerCard = false;
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
                //StartCoroutine(DelaySelectCreatedTowarCardSetTrue());

                if (towerPoints.Points.Count != 0)
                    StartCoroutine(DelayRemovePointSpawn(pointSpawnTower));
            }
        }
    }

    /*IEnumerator DelaySelectCreatedTowarCardSetTrue()
    {
        yield return new WaitForSeconds(4f);
        if (towerPoints.Points.Count != 0)
        {
            SelectCreatedTowarCardSetTrue();
        }
    }*/
    /*void SelectCreatedTowarCardSetTrue()
    {
        towerPoints.isSelectCreatedTowerCard = true;
    }*/

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