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

    public GameObject projectile;
    private int attackType;
    private bool isAttacking;
    private bool isChasing;
    private bool isHealing;
    private bool isBurstAttacking;
    private float chaseDelay;
    public float healDelay = 2f;
    private bool isHealingRightNow;
    private bool hasMultiShooted;
    private bool isMultiShooting;

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
            attackType = Random.Range(1, 9);
            if (attackType == 1) {
                isHealing = true;
                isGoingToHeal = true;
                isAttacking = true;
                healDelay = 5;
            }
            if (attackType >= 2 && attackType <= 5) {
                isChasing = true;
                isAttacking = true;
                chaseDelay = Random.Range(1, 10);
            }
            if (attackType == 6) {
                hasMultiShooted = false;
                isMultiShooting = false;
                isAttacking = true;
                isBurstAttacking = true;
            }
            if (attackType == 7 | attackType == 8) { }
            if (attackType == 9) { }
        }
        if (isChasing)
        {
            ChaseAttack();
        }
        if (isHealing)
        {
            Heal();
        }
        if (isBurstAttacking)
        {
            BurstAttack();
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
    private void BurstAttack()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (!isMultiShooting)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z)), 1);
            Debug.Log("rotated");
            //Instantiate(projectile, transform.position, transform.rotation);
            StartCoroutine("Burst", 3f);
        }
        if (hasMultiShooted)
        {
            isAttacking = false;
            isBurstAttacking = false;
        }

    }
    private void Heal()
    {
        target = GameObject.FindGameObjectWithTag("FrightflyHeal");
        if (this.gameObject.transform.position != target.transform.position)
        {
            if (isGoingToHeal) {
                isHealingRightNow = false;
                GoToHeal();
            }
            if (!isGoingToHeal && !isHealingRightNow)
            {
                isHealingRightNow = true;
                StartCoroutine("HealDelay");
                //isAttacking = false;
                //isHealing = false;
            }
        }
    }

    IEnumerator HealDelay()
    {
        yield return new WaitForSecondsRealtime(healDelay / 4);
        FrightflyHP.hp += 10f;
        yield return new WaitForSecondsRealtime(healDelay / 4);
        FrightflyHP.hp += 10f;
        yield return new WaitForSecondsRealtime(healDelay / 4);
        FrightflyHP.hp += 10f;
        yield return new WaitForSecondsRealtime(healDelay / 4);
        FrightflyHP.hp += 10f;
        Debug.Log(FrightflyHP.hp);
        isAttacking = false;
        isHealing = false;
    }

    IEnumerator Burst(float time)
    {
        isMultiShooting = true;
        Instantiate(projectile, transform.position, transform.rotation);
        yield return new WaitForSeconds(time);
        Instantiate(projectile, transform.position, transform.rotation);
        yield return new WaitForSeconds(time);
        Instantiate(projectile, transform.position, transform.rotation);
        yield return new WaitForSeconds(time);
        hasMultiShooted = true;
    }
}