using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreCubes : MonoBehaviour
{
    public GameObject centre;
    public float speed;

    void Update()
    {
        transform.LookAt(centre.transform);
        transform.Translate(new Vector3(speed, 0, 0), Space.Self);
    }

    void OnCollisionEnter(Collision coll)
    {
        coll.transform.parent = transform;
    }

    private void OnCollisionStay(Collision coll)
    {
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.Space))
        {
            coll.transform.parent = null;
        } else
        {
            coll.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        coll.transform.parent = null;
    }
}
