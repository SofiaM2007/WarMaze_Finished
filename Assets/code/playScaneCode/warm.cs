using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warm : MonoBehaviour
{
    private float _speed = 5.0f; // Скорость движения
    private Rigidbody2D rb;
    private gameController g;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.WakeUp();

        g = FindObjectOfType<gameController>();
        Debug.Log(g.player_speed);

        rb.gravityScale = 0;
        rb.drag = 0;         // Отключаем торможение
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement;

        if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical)){
            movement = new Vector2(moveHorizontal, 0.0f);
        }
        else{
            movement = new Vector2(0.0f, moveVertical);
        }

        // скорость
        rb.velocity = movement * _speed;

        // поворот головы
        if (movement.sqrMagnitude > 0)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            rb.rotation = angle-90;
        }
    }
}
