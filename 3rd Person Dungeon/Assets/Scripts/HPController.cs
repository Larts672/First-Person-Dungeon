using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    private GameObject player;
    private GameObject camera;

    public float HP = 150f; // Хп игрока
    public float fixedHP;
    public float maxHP;

    private Image hpBar;
    private Image hpBarBack;
    private Text hp;

    private bool isInLava = false;
    private float lavaDamageDelay = 0.0125f;

    private bool immortal = false;

    void Start()
    {
        maxHP = HP;
        fixedHP = HP;

        player = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera");

        hpBar = GameObject.Find("Player Hp Bar").GetComponent<Image>();
        hpBarBack = GameObject.Find("Player Hp Bar Back").GetComponent<Image>();
        hp = GameObject.Find("Player Hp").GetComponent<Text>();
        
    }

    void Update()
    {
        if (Input.GetKey("i"))
        {
            immortal = true;
        }
        if (immortal)
        {
            HP = fixedHP;
        }

        Death();

        hpBar.rectTransform.position = new Vector3((885 / 1.3f) - ((fixedHP - HP) / 2), 20f, 0f);
        hpBarBack.rectTransform.position = new Vector3(885 / 1.3f, 20f, 0f);
        hp.rectTransform.position = new Vector3(885 / 1.3f, 20f, 0f);

        hpBar.rectTransform.sizeDelta = new Vector2(HP, 20f);
        hpBarBack.rectTransform.sizeDelta = new Vector2(fixedHP, 20f);

        hp.text = HP.ToString();

        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            HP += 1;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            HP -= 1;
        }

        if (isInLava)
        {
            lavaDamageDelay -= Time.deltaTime;
        }
        if (lavaDamageDelay <= 0 && HP > 0)
        {
            HP -= 1;
            lavaDamageDelay = 0.0125f;
        }
    }

    private void Death()
    {
        if (HP <= 0)
        {
            HP = 0;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<MouseLook>().enabled = false;
            player.GetComponentInChildren<Attack>().enabled = false;
            camera.GetComponent<MouseLook>().enabled = false;

            Cursor.lockState = CursorLockMode.None;
            DontDestroyOnLoad(GameObject.Find("Finish Data"));
            GameObject.Find("Finish Data").GetComponent<FinishData>().finishType = 0;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lava")
        {
            isInLava = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Lava")
        {
            isInLava = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lava")
        {
            isInLava = false;
        }
    }
}
