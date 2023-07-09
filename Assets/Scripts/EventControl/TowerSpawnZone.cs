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
        // Kule prefabýný spawn noktasýnýn transformuna eþitleyerek kuleyi spawn noktasýna taþý
        GameObject tower = Instantiate(towerPrefab, gameObject.transform.position, Quaternion.identity);

        // Kule prefabýnýn yükseklik deðerini ayarla
        Bounds bounds = CalculateBounds(tower);
        float towerHeight = bounds.size.y;
        tower.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + towerHeight / 2f, gameObject.transform.position.z);

        // Kule prefabýnýn alt kýsmýnýn yerin altýna girmesini engelle
        float groundHeight = 0f; // Yer seviyesinin yükseklik deðeri
        if (tower.transform.position.y - towerHeight / 2f < groundHeight)
        {
            tower.transform.position = new Vector3(tower.transform.position.x, groundHeight + towerHeight / 2f, tower.transform.position.z);
        }

        //Vector3 _vector = gameObject.transform.position;
        //_vector.y = _vector.y + (_vector.y - towerPrefab.transform.Find("Pivot").position.y);
        //var pivot = Instantiate(towerPrefab, _vector, Quaternion.identity);
        ////Vector3 _vector = pivot.transform.position;
        ////_vector.y = 2 * _vector.y; //+ (_vector.y - towerPrefab.transform.Find("Pivot").position.y);

        //pivot.transform.position = _vector;


        towerPoints.isDone = false;
        towerPoints.isSelectCreatedTowerCard = false;
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