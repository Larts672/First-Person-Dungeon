using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb; // Рижидбоди
    public float speed = 6f; // Скорость
    public float sprintSpeed = 9f; // Скорость на шифте
    private float standartSpeed;
    public float jumpForce = 3f; // Сила прыжка
    public bool isCursorHiden; // Скрыт ли курсор
    public bool isGrounded; // Персонаж на земле?

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        standartSpeed = speed;
        Cursor.lockState = CursorLockMode.Locked; // Блокируем и прячем курсор
        isCursorHiden = true;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        } else
        {
            speed = standartSpeed;
        }

        Vector3 move = transform.right * x * speed * Time.deltaTime + transform.forward * z * speed * Time.deltaTime;

        //transform.Translate(move, Space.World);

        rb.MovePosition(rb.position + move);

        Jump();

        if (Input.GetKeyDown(KeyCode.Escape) && isCursorHiden)
        {
            Cursor.lockState = CursorLockMode.None;
            isCursorHiden = false;
        }
        if (Input.GetMouseButtonDown(0) && !isCursorHiden)
        {
            Cursor.lockState = CursorLockMode.Locked;
            isCursorHiden = true;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce * rb.mass * 100);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpdate(collision, true);
    }
    /*void OnCollisionExit(Collision collision)
    {
        IsGroundedUpdate(collision, false);
    }*/
    private void IsGroundedUpdate(Collision collision, bool value)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = value;
        }
    }
}
