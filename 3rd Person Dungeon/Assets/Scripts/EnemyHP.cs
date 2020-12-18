using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float maxMoney;
    public float minMoney;
    public Animator animator;
    public float hp;
    public FinishData _FinishData;
    
    void Start()
    {
        _FinishData = GameObject.Find("Finish Data").GetComponent<FinishData>();
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
            int droppedMoney = (int)Random.Range(minMoney, maxMoney);
            _FinishData.money += droppedMoney;

        }
    }
}
