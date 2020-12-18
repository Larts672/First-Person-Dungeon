using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishData : MonoBehaviour
{
    public int finishType; // Как завершилась игра: 0 - игрок умер, 1 - игрок прошёл игру
    public int money; // Деньгэ

    private void Start()
    {
        money = 0;
        finishType = -1;
    }

    private void Update()
    {
        if (GameObject.Find("Money") != null)
        {
            GameObject.Find("Money").GetComponent<Text>().text = money.ToString();
        }
    }
}
