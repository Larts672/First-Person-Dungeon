using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    private GameObject player;
    public GameObject productInfo;

    public GameObject originalFlameSword;
    private GameObject flameSword;
    public GameObject[] Artifacts;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        productInfo = GameObject.Find("Product Info");
        productInfo.GetComponent<MeshRenderer>().enabled = false;
        productInfo.transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);

        originalFlameSword.transform.position = transform.position;
        flameSword = Instantiate(originalFlameSword);
        flameSword.transform.rotation = Quaternion.Euler(-30f, 0f, 0f);
        flameSword.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
    }

    void Update()
    {
        productInfo.transform.LookAt(player.transform);
        if (Vector3.Distance(player.transform.position, productInfo.transform.position) <= 2.25f)
        {
            productInfo.GetComponent<MeshRenderer>().enabled = true;
        } else
        {
            productInfo.GetComponent<MeshRenderer>().enabled = false;
        }
        flameSword.transform.Rotate(0f, 0.5f, 0f, Space.Self);
    }
}
