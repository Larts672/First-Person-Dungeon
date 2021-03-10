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
    public bool isGoingToHeal;
    public EnemyHP FrightflyHP;

    private int attackType;
    private bool isAttacking;
    private bool isChasing;
    private bool isHealing;
    private float chaseDelay;
    private float healDelay;

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
            if (attackType == 1) {
                isHealing = true;
                isGoingToHeal = true;
                isAttacking = true;
                healDelay = 5;
                ;
            }
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
        if (isHealing)
        {
            Heal();
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

    private void GoToHeal()
    {
        transform.position = Vector3.MoveTowards
                (transform.position,
                new Vector3(transform.position.x, target.transform.position.y, transform.position.z),
                0.01f * speed);
        if (this.gameObject.GetComponentInChildren<EnemyHP>().hp > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z)), 0.02f);
            frightflyAnim.SetBool("Fly Forward", true);
            transform.position = Vector3.MoveTowards
                (transform.position,
                new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z),
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
        target = GameObject.FindGameObjectWithTag("Player");
        chaseDelay -= Time.deltaTime;
        if (chaseDelay <= 0)
        {
            isAttacking = false;
            isChasing = false;
        }
    }
    private void Heal()
    {
        target = GameObject.FindGameObjectWithTag("FrightflyHeal");
        if (this.gameObject.transform.position != target.transform.position)
        {
            if (isGoingToHeal) {
                GoToHeal();
            }
            if (!isGoingToHeal && healDelay>0)
            {
                healDelay -= Time.deltaTime;
                FrightflyHP.hp += 100f;
            }
            if(!isGoingToHeal && healDelay < 0)
            {
                isAttacking = false;
                isHealing = false;
            }            
        }
    }
}
