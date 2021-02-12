using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrightflyAI : MonoBehaviour
{
    public float speed = 5f;

    private GameObject target;
    public Animator frightflyAnim;
    public float biteDistance;
    public float stingDistance;
    private float biteAttackDelay; // Кулдаун укуса
    private float stingAttackDelay; // Кулдаун атаки жалом
    public bool canBiteAttack = true;
    public bool canStingAttack = true;

    private int attackType;
    private bool isAttacking;
    private bool isChasing;
    private float chaseDelay;

    void Start()
    {
        isAttacking = false;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Attack();
        if (isChasing)
        {
            Chase();
            StingAttack();
        }
    }

    private void Attack() {
        if (!isAttacking)
        {
            attackType = Random.Range(1, 7);
            if (attackType == 1) { }
            if (attackType == 2 | attackType == 3) {
                isChasing = true;
                isAttacking = true;
                chaseDelay = Random.Range(1, 10);
            }
            if (attackType == 4) { }
            if (attackType == 5 | attackType == 6) { }
            if (attackType == 7) { }
        }
        if (isChasing)
        {
            ChaseAttack();
        }
    }

    private void Chase()
    {
        transform.position = Vector3.MoveTowards
                (transform.position,
                new Vector3(transform.position.x, target.transform.position.y+1, transform.position.z),
                0.01f * speed);
        if (Vector3.Distance(target.transform.position, transform.position) >= stingDistance && this.gameObject.GetComponentInChildren<EnemyHP>().hp > 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, target.transform.position.z - transform.position.z));
            frightflyAnim.SetBool("Fly Forward", true);
            transform.position = Vector3.MoveTowards
                (transform.position,
                new Vector3(target.transform.position.x+4f, transform.position.y, target.transform.position.z),
                0.01f * speed);
        }
        else
        {
            frightflyAnim.SetBool("Fly Forward", false);
        }
    }

    private void StingAttack()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < stingDistance && this.gameObject.GetComponentInChildren<EnemyHP>().hp > 0)
        {
            if (canStingAttack)
            {
                frightflyAnim.SetTrigger("Sting Attack");
                canStingAttack = false;
                stingAttackDelay = Random.Range(3.2f, 4.5f);
            }
            if (!canStingAttack)
            {
                transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, target.transform.position.z - transform.position.z)), Time.deltaTime * speed);
                stingAttackDelay -= Time.deltaTime;
                if (stingAttackDelay <= 0)
                {
                    canStingAttack = true;
                }
            }
        }
    }

    private void BiteAttack()
    {

    }

    private void ChaseAttack()
    {
        chaseDelay -= Time.deltaTime;
        if (chaseDelay <= 0)
        {
            isAttacking = false;
            isChasing = false;
        }
    }
}
