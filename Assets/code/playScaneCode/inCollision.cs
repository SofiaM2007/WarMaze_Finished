using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("evilBullet"))  // Проверка на столкновение с пулей
        {
            Destroy(collision.gameObject); // Удаляем пулю
        }
    }
}
