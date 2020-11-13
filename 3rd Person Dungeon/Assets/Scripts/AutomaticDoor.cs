using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    private bool opened, opening, closing;
    public float speed;

    void Start()
    {
        opened = false;
        opening = false;
        closing = false;
    }

    void Update()
    {
        if (opening && transform.position.y <= 4.125f)
        {
            transform.Translate(0f, speed * Time.deltaTime, 0f);
        }
        if (closing && transform.position.y >= 1.375f)
        {
            transform.Translate(0f, -speed * Time.deltaTime, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            opening = true;
            closing = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            closing = true;
            opening = false;
        }
    }
}
