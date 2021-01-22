using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Product : MonoBehaviour
{
    private GameObject player;
    private GameObject finishData;
    public GameObject productInfo;
    public Canvas canvas;
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
        productInfo.GetComponent< TextMeshProUGUI>().text = "";

        if (productNumber == 0) { currentPrice = healPotionPrice; currentName = potionOfHealName; };
        if (productNumber == 1) { currentPrice = holyGrenadePrice; currentName = holyGrenadeName; };


        product = Instantiate(Artifacts[productNumber], transform);
        product.transform.rotation = Quaternion.Euler(-30f, 0f, 0f);
    }

    void Update()
    {
        canvas.GetComponent<RectTransform>().LookAt(player.transform);
        if (Vector3.Distance(player.transform.position, canvas.GetComponent<RectTransform>().position) <= 3.5f)
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
            productInfo.GetComponent<TextMeshProUGUI>().enabled = true;
        } else
        {
            productInfo.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        //flameSword.transform.Rotate(0f, 0.5f, 0f, Space.Self);
        if (!isSold)
        {
            productInfo.GetComponent<TextMeshProUGUI>().text  = currentName + ": " + "<b><color=yellow>"+currentPrice+"</color></b>" + " coins";
        }
        else
        {
            productInfo.GetComponent<TextMeshProUGUI>().text = currentName;
        }
        product.transform.Rotate(0f, 0.5f, 0f, Space.Self);
    }
}
