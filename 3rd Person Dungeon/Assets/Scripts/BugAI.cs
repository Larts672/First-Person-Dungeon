using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI : MonoBehaviour
{

    public Animator bugAnim; // Аниматор
    public GameObject target; // Цель (не "player", т.к. может будут миньоны)
    public float seeDistance = 50f; // Дальность видимости
    public float attackDistance = 4f; // Дальность атаки
    public float speed = 4f; // Скорость

    // Атака
    public float stabAttackDelay; // Кулдаун рывка
    public bool canStabAttack = true;
    public float smashAttackDelay; // Кулдаун атаки по площади
    public bool canSmashAttack = true;


    void Start()
    {
        bugAnim = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    void Update()
    {

        Chase(); // Преследование

        Attack(); // Атака
    }

    // stabAttackDelay = Random.Range(1.7f, 2.9f);
    // smashAttackDelay = Random.Range(3.2f, 4.4f);

    private void Chase()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= seeDistance && Vector3.Distance(target.transform.position, transform.position) >= attackDistance && this.gameObject.GetComponent<EnemyHP>().hp > 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z));
            bugAnim.SetBool("Walk Forward", true);
            transform.position = Vector3.MoveTowards
                (transform.position,
                new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z),
                0.01f * speed);
        }
        else
        {
            bugAnim.SetBool("Walk Forward", false);
        }
    }

    private void Attack()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= attackDistance && this.gameObject.GetComponent<EnemyHP>().hp > 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z));
            int attackType = Random.Range(0, 2); // Выбор способа атаки

            if (attackType == 0 && canStabAttack)
            {
                bugAnim.SetBool("Stab Attack", true);
                canStabAttack = false;
                stabAttackDelay = Random.Range(1.7f, 2.9f);
            }
            if (attackType == 1 && canSmashAttack)
            {
                bugAnim.SetBool("Smash Attack", true);
                canSmashAttack = false;
                smashAttackDelay = Random.Range(3.2f, 4.4f);
            }

            if (!canStabAttack)
            {
                stabAttackDelay -= Time.deltaTime;
                if (stabAttackDelay <= 0)
                {
                    canStabAttack = true;
                }
            }
            if (!canSmashAttack)
            {
                smashAttackDelay -= Time.deltaTime;
                if (smashAttackDelay <= 0)
                {
                    canSmashAttack = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            
        }
    }
}
