using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private bool isPlayerHere;
    public GameObject _Enemies;

    void Start()
    {
        
    }

    void Update()
    {
        if (isPlayerHere)
        {
            _Enemies.SetActive(true);
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
        if(other.gameObject.tag == "Player")
        {
            isPlayerHere = true;
        }
        else
        {
            isPlayerHere = false;
        }
    }
}
