using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCentre : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            GameObject.FindWithTag("Enemy").GetComponent<FrightflyAI>().isGoingToHeal = false;
        }
    }
}
