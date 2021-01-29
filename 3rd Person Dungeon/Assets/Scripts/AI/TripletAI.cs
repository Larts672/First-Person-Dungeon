using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripletAI : MonoBehaviour
{
    public Animator tripletAnim; // Аниматор
    public GameObject target; // Цель (не "player", т.к. может будут миньоны)
    public float seeDistance = 50f; // Дальность видимости
    public float maxRangedAttackDistance;
    public float minRangedAttackDistance;
    public float meleeAttackDistance;
    public float speed = 4f; // Скорость
    public float rotateSpeed = 2f;


    // Атака
    public GameObject leftAttackPlace;
    public GameObject mediumAttackPlace;
    public GameObject rightAttackPlace;
    public GameObject projectile;

    private bool canShoot;
    private bool canBite;
    public float attackDelay = 1.5f;
    public Vector3 rotationVec;
    private float attackFixedDelay;

    public float biteDelay = 1.5f;
    private float fixedBiteDelay;

    void Start()
    {
        attackFixedDelay = attackDelay;
        fixedBiteDelay = biteDelay;
        tripletAnim = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    void Update()
    {
        if((Vector3.Distance(target.transform.position, transform.position) <= maxRangedAttackDistance))
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
        if(Vector3.Distance(target.transform.position, transform.position) <= minRangedAttackDistance)
        {
            canShoot = false;
        }
        Chase();

        Attack();
    }

    private void Attack()
    {
        if (attackDelay >= 0)
        {
            attackDelay -= Time.deltaTime;
        }
        if (canShoot && this.gameObject.GetComponent<EnemyHP>().hp > 0&& attackDelay < 0)
        {

                tripletAnim.SetTrigger("Projectile Attack");
                Instantiate(projectile, leftAttackPlace.transform.position, leftAttackPlace.transform.rotation);
                Instantiate(projectile, mediumAttackPlace.transform.position, mediumAttackPlace.transform.rotation);
                Instantiate(projectile, rightAttackPlace.transform.position, rightAttackPlace.transform.rotation);

            attackDelay = attackFixedDelay;
            

        }

        if (Vector3.Distance(target.transform.position, transform.position) <= meleeAttackDistance && this.gameObject.GetComponent<EnemyHP>().hp > 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z));

            if (canBite)
            {
                tripletAnim.SetBool("Walk Forward", false);
                tripletAnim.SetBool("Bite Attack", true);
                //canBite = false;
                biteDelay = Random.Range(1.7f, 2.9f);
            }

            if (!canBite)
            {
                biteDelay -= Time.deltaTime;
                if (biteDelay <= 0)
                {
                    canBite = true;
                }
            }

        }


    }

    private void Chase()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z)), rotateSpeed / 100);
        //rotationVec = new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z);
        //transform.rotation = Quaternion.LookRotation(rotationVec*0.001f);
        if (Vector3.Distance(target.transform.position, transform.position) <= seeDistance && !canShoot && (Vector3.Distance(target.transform.position, transform.position) >= meleeAttackDistance) && this.gameObject.GetComponent<EnemyHP>().hp > 0)
        {
            tripletAnim.SetBool("Walk Forward", true);
            transform.position = Vector3.MoveTowards
                (transform.position,
                new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z),
                0.01f * speed);
        }
        else
        {
            tripletAnim.SetBool("Walk Forward", false);
        }
    }
}
