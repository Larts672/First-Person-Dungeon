using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    private GameObject player;
    private GameObject finishData;
    public GameObject productInfo;
    public int healPotionPrice;
    public int holyGrenadePrice;
    public string potionOfHealName = "Potion of Heal";
    public string holyGrenadeName = "Holy Grenade";

    private string currentName;
    private int currentPrice;
    public GameObject originalFlameSword;
    private int productNumber;
    private GameObject product;
    private GameObject flameSword;
    public GameObject[] Artifacts;
    public bool isSold = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        finishData = GameObject.Find("Finish Data");

        productNumber = Random.Range(0, Artifacts.Length);
        productInfo = GameObject.Find("Product Info");
        productInfo.GetComponent<MeshRenderer>().enabled = false;
        productInfo.transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);

        if (productNumber == 0) { currentPrice = healPotionPrice; currentName = potionOfHealName; };
        if (productNumber == 1) { currentPrice = holyGrenadePrice; currentName = holyGrenadeName; };


        product = Instantiate(Artifacts[productNumber], transform);
        product.transform.rotation = Quaternion.Euler(-30f, 0f, 0f);
        //product.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);

        /*originalFlameSword.transform.position = transform.position;
        flameSword = Instantiate(originalFlameSword);
        flameSword.transform.rotation = Quaternion.Euler(-30f, 0f, 0f);
        flameSword.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);*/
    }

    void Update()
    {
        productInfo.transform.LookAt(player.transform);
        if (Vector3.Distance(player.transform.position, productInfo.transform.position) <= 2.25f)
        {
            if (Input.GetKeyDown("e") && finishData.GetComponent<FinishData>().money>=currentPrice && !isSold)
            {
                product.SetActive(false);
                currentName = "Thank you";
                if (productNumber == 0) { player.GetComponent<HPController>().HP = Mathf.Clamp(player.GetComponent<HPController>().HP + 50, 0, player.GetComponent<HPController>().maxHP);
                }
                finishData.GetComponent<FinishData>().money -= currentPrice;
                isSold = true;
            }
            productInfo.GetComponent<MeshRenderer>().enabled = true;
        } else
        {
            productInfo.GetComponent<MeshRenderer>().enabled = false;
        }
        //flameSword.transform.Rotate(0f, 0.5f, 0f, Space.Self);
        if (!isSold)
        {
            productInfo.GetComponent<TextMesh>().text = currentName + ": " + "<b><color=yellow>"+currentPrice+"</color></b>" + " coins";
        }
        else
        {
            productInfo.GetComponent<TextMesh>().text = currentName;
        }
        product.transform.Rotate(0f, 0.5f, 0f, Space.Self);
    }
}
