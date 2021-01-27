using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaSooterAI : MonoBehaviour
{
    public Animator peaAnim; // Аниматор
    public GameObject target; // Цель (не "player", т.к. может будут миньоны)
    public float seeDistance = 50f; // Дальность видимости
    public float attackDistance = 1500f; // Дальность атаки
    public float speed = 4f; // Скорость
    public float rotateSpeed = 2f;

    // Атака
    public Transform attackPlace;
    public GameObject projectile;
    public bool canAttack = true;
    public float attackDelay = 1.5f;
    public Vector3 rotationVec;
    private float attackFixedDelay;

    void Start()
    {
        attackFixedDelay = attackDelay;
        peaAnim = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    void Update()
    {
        Chase();

        Attack();
    }

    private void Attack()
    {
        if (attackDelay>=0)
        {
            attackDelay -= Time.deltaTime;
        }
        if (Vector3.Distance(target.transform.position, transform.position) <= attackDistance && this.gameObject.GetComponent<EnemyHP>().hp > 0)
        {
            if (attackDelay<0)
            {
                peaAnim.SetTrigger("Projectile Attack");
                Instantiate(projectile, attackPlace.position, transform.rotation);

                attackDelay = attackFixedDelay;
            }

        }
    }

    private void Chase()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z)), rotateSpeed/100);
        //rotationVec = new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z);
        //transform.rotation = Quaternion.LookRotation(rotationVec*0.001f);
        if (Vector3.Distance(target.transform.position, transform.position) <= seeDistance && Vector3.Distance(target.transform.position, transform.position) >= attackDistance && this.gameObject.GetComponent<EnemyHP>().hp > 0)
        {
            peaAnim.SetBool("Walk Forward", true);
            transform.position = Vector3.MoveTowards
                (transform.position,
                new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z),
                0.01f * speed);
        }
        else
        {
            peaAnim.SetBool("Walk Forward", false);
        }
    }
}
