using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private GameObject[] grids;
    private bool isPlayerHere;
    public GameObject _Enemies;
    public bool enemiesSlayed;

    public bool battle = false;
    public bool cleaned = false;

    void Start()
    {
        grids = GameObject.FindGameObjectsWithTag("Grid");
    }

    void Update()
    {
        if (isPlayerHere)
        {
            if (!cleaned)
            {
                battle = true;
                _Enemies.SetActive(true);
            }
            if (battle == true)
            {
                foreach (GameObject i in grids)
                {
                    if (i.transform.position.y > 1)
                    {
                        i.transform.Translate(new Vector3(0, -3f, 0));
                    }
                }
                for (int i = 0; i < transform.childCount; i++)
                {
                    enemiesSlayed = true;
                    if (transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        enemiesSlayed = false;
                    }
                }
                if (enemiesSlayed) {
                    battle = false;
                    cleaned = true;
                        }
            }
            /*if (!enemiesSlayed)
            {
                foreach (GameObject i in grids)
                {
                    if (i.transform.position.y > 1)
                    {
                        i.transform.Translate(new Vector3(0, -3f, 0));
                    }
                }
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                enemiesSlayed = true;
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    enemiesSlayed = false;
                }
            }
            if(enemiesSlayed == true)
            {
                foreach (GameObject i in grids)
                {
                    if (i.transform.position.y < 4)
                    {
                        i.transform.Translate(new Vector3(0, 3f, 0));
                    }
                }
            } */

        }
        else
        {
            foreach (GameObject i in grids)
            {
                if (i.transform.position.y < 4)
                {
                    i.transform.Translate(new Vector3(0, 3f, 0));
                }
            }
        }
    }

    /*public void PlayerChecker(GameObject enemy)
    {
        if (isPlayerHere) {
            enemy.GetComponentInChildren<MessengerScript>().WakeUp();
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerHere = true;
            
        }
        else
        {
            
            isPlayerHere = false;
        }
    }
}
