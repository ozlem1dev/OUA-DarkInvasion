using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPoints : MonoBehaviour
{
    public List<GameObject> Points = new List<GameObject>();
    public GameObject cameraa;
    public GameObject soldier;
    public GameObject characterPanel;
    public GameObject towerPanel;
    public GameObject bookPanel;
    public  bool isSelectCreatedTowerCard=false;
    public bool isDone = false;
    public static int currentIndex = 0;

    void Update()
    {
        if (isSelectCreatedTowerCard && !isDone)
        {
            towerPanel.SetActive(true);
            characterPanel.SetActive(false);
            bookPanel.SetActive(false);
            isDone = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


            FollowSpawnPoint();
        }
    }

    public void FollowSpawnPoint()
    {
        if (Points.Count - 1 < currentIndex)
            currentIndex = 0;

        if (Points.Count != 0 && Points != null && Points[currentIndex] != null)
        {
            cameraa.GetComponent<CinemachineFreeLook>().Follow = Points[currentIndex].transform;
            cameraa.GetComponent<CinemachineFreeLook>().LookAt = Points[currentIndex].transform;
        }
    }

    public void FollowCharacter()
    {
        cameraa.GetComponent<CinemachineFreeLook>().Follow = soldier.transform;
        cameraa.GetComponent<CinemachineFreeLook>().LookAt = soldier.transform;

        towerPanel.SetActive(false);
        characterPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Next()
    {
        if (Points.Count != 0)
        {
            currentIndex = currentIndex >= Points.Count - 1 ? 0 : ++currentIndex;
            FollowSpawnPoint();
        }
    }

    public void Back()
    {
        if (Points.Count != 0)
        {
            currentIndex = currentIndex <= 0 ? Points.Count - 1 : --currentIndex;
            FollowSpawnPoint();
        }
    }
    
}
