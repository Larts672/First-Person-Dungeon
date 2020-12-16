using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private GameObject[] grids;
    private bool isPlayerHere;
    public GameObject _Enemies;
    public bool enemiesSlayed;
    public GameObject[] enemiesArr;

    public bool battle = false;
    public bool cleaned = false;

    void Start()
    {
        enemiesArr = new GameObject[_Enemies.transform.childCount];
        grids = GameObject.FindGameObjectsWithTag("Grid");
        for(int i=0; i<_Enemies.transform.childCount; i++){
            enemiesArr[i] = _Enemies.transform.GetChild(i).gameObject;
        }
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
                enemiesSlayed = true;
                foreach (GameObject i in enemiesArr)
                {
                    if (i.activeSelf)
                    {
                        enemiesSlayed = false;
                    }
                }
                if (enemiesSlayed) {
                    battle = false;
                    cleaned = true;
                    foreach (GameObject i in grids)
                    {
                        if (i.transform.position.y < 4)
                        {
                            i.transform.Translate(new Vector3(0, 3f, 0));
                        }
                    }
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
        if (cleaned)
        {
            /*foreach (GameObject i in grids)
            {
                if (i.transform.position.y < 4)
                {
                    i.transform.Translate(new Vector3(0, 3f, 0));
                }
            }*/
        }
    }

    /*public void PlayerChecker(GameObject enemy)
    {
        if (isPlayerHere) {
            enemy.GetComponentInChildren<MessengerScript>().WakeUp();
        }
    }*/
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerHere = true;
            
        }
    }
}
