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

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Chase();
        StingAttack();
    }

    private void Chase()
    {
        if (Vector3.Distance(target.transform.position, transform.position) >= stingDistance && this.gameObject.GetComponentInChildren<EnemyHP>().hp > 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z));
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
                transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z)), Time.deltaTime * speed);
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
}
