using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDeliteBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("evilBullet"))  // Проверка на столкновение с враждебной пулей
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
