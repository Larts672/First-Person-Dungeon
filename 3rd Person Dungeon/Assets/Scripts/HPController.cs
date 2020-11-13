using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    public float HP = 150f; // Хп игрока

    private Image hpBar; // Полоска хп
    private Text hp; // Отображаемые хп

    private float lavaDamageDelay = 0.2f;
    private float lavaFixedDelay = 0.2f;

    void Start()
    {
        hpBar = GameObject.Find("Player Hp Bar").GetComponent<Image>();
        hp = GameObject.Find("Player Hp").GetComponent<Text>();
    }

    void Update()
    {
        hpBar.rectTransform.position = new Vector3(885/1.3f, 20f, 0f);
        hpBar.rectTransform.sizeDelta = new Vector2(HP, 20f);

        hp.rectTransform.position = new Vector3(885 / 1.3f, 20f, 0f);
        hp.text = HP.ToString(); // Можно string.Format("{0:0.0}", HP)

        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            HP += 1;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            HP -= 1;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lava")
        {
            while (HP > 0)
            {
                if (lavaDamageDelay <= 0) 
                {
                    HP -= 0.5f;
                    lavaDamageDelay = lavaFixedDelay;
                }
                lavaDamageDelay -= Time.deltaTime;
            }
        }
    }
}
