using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaSooterAI : MonoBehaviour
{
    public Animator peaAnim; // Аниматор
    public GameObject target; // Цель (не "player", т.к. может будут миньоны)
    public float seeDistance = 50f; // Дальность видимости
    public float attackDistance = 10f; // Дальность атаки
    public float speed = 4f; // Скорость

    // Атака
    public float stabAttackDelay; // Кулдаун рывка
    public bool canStabAttack = true;
    public float smashAttackDelay; // Кулдаун атаки по площади
    public bool canSmashAttack = true;

    void Start()
    {
        peaAnim = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    void Update()
    {
        Chase();

        //Attack();
    }

    private void Chase()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z));
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
