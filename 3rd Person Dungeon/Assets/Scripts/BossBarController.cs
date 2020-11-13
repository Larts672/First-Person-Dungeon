using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBarController : MonoBehaviour
{
    private GameObject Boss;

    public float bossHp;
    private float fixedBossHp;

    private Image bossBar;
    private Image bossBarBack;
    private Text bossHpText;

    void Start()
    {
        bossHp = GameObject.Find("Boss").GetComponent<EnemyHP>().hp;

        bossBar = GameObject.Find("BossBar").GetComponent<Image>();
        bossBarBack = GameObject.Find("BossBar Back").GetComponent<Image>();
        bossHpText = GameObject.Find("Boss Hp").GetComponent<Text>();

        fixedBossHp = bossHp;
    }

    void Update()
    {
        bossBar.rectTransform.position = new Vector3((885 / 1.3f) - ((fixedBossHp-bossHp)/4) , 570f, 0f);
        bossBarBack.rectTransform.position = new Vector3(885 / 1.3f, 570f, 0f);
        bossHpText.rectTransform.position = new Vector3(885 / 1.3f, 570f, 0f);

        bossBar.rectTransform.sizeDelta = new Vector2(bossHp / 2, 20f);
        bossBarBack.rectTransform.sizeDelta = new Vector2(fixedBossHp / 2, 20f);

        bossHpText.text = bossHp.ToString();

        if (bossHp <= 0)
        {
            bossHp = 0;
            GameObject.Find("Boss").SetActive(false);
        }
    }
}
