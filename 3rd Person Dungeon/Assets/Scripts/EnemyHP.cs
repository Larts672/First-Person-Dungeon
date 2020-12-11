using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public Animator animator;
    public float hp;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("HP: " + hp);
    }

    void Update()
    {
        Death();
    }


    public void TakeDamage(float damage)
    {
        animator.SetBool("Take Damage", true);
        this.hp -= damage;
        Debug.Log("HP: " + hp);
    }

    private void Death()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
