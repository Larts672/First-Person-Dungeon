using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator;
    public GameObject sword;

    public float dmg = 1f;
    private bool wasEnemyHitted = false;
    private bool isAttack = false;
    private bool startDelay;
    private bool delayFinished;
    private float delay = 0.35f;

    void Start()
    {
        animator = GameObject.Find("PlSword").GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startDelay = true;
            animator.SetBool("Attack", true);
            animator.SetBool("Idle", false);
        } else
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Idle", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Attack"))
        {
            isAttack = true;
        } else
        {
            isAttack = false;
            wasEnemyHitted = false;
        }
        if (startDelay)
        {
            delay -= Time.deltaTime;
        }
        if (delay <= 0)
        {
            startDelay = false;
            delayFinished = true;
            delay = 0.35f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy" | other.gameObject.tag == "Frightfly") && !wasEnemyHitted && isAttack && delayFinished)
        {
            other.GetComponent<EnemyHP>().TakeDamage(dmg);
            wasEnemyHitted = true;
            delayFinished = false;
        }
    }
}